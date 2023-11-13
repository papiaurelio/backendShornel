using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenServices;
        private readonly IMapper _mapper;
        private readonly IGenericSeguridadRepository<Usuario> _seguridadRepository;
        private readonly RoleManager<IdentityRole> _rolManager;

        //private readonly IPasswordHasher<Usuario> _passwordHasher;
        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            ITokenService tokenServices, IMapper maper, IGenericSeguridadRepository<Usuario> seguridadRepository,
            RoleManager<IdentityRole> rolManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = maper;
            _seguridadRepository = seguridadRepository;
            _rolManager = rolManager;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);
            if (string.IsNullOrEmpty(loginDto.Email) || usuario == null)
            {
                return Unauthorized(new CodeErrorResponse(401, "Email incorrecto"));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false);
            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401, "Contraseña incorrecta"));
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                Username = usuario.UserName,
                //Token = _tokenServices.CreateToken(usuario),
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos
            };

        }

        [HttpPost("registrar")]

        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {
            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.Username,
                Nombres = registrarDto.Nombres,
                Apellidos = registrarDto.Apellidos
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, "Error al registrar el usuario"));
            }

            return new UsuarioDto
            {
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                //Token = _tokenServices.CreateToken(usuario),
                Email = usuario.Email,
                Username = usuario.UserName,

            };
        }

        [HttpPut("actualizar")]
        public async Task<ActionResult<UsuarioDto>> ActualizarUsario(UsuarioDto usuarioDto)
        {
            //var usuario = await _userManager.FindByIdAsync(id);

            var usuario = await _userManager.BuscarUsarioById(User);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            usuario.Nombres = usuarioDto.Nombres;
            usuario.Apellidos = usuarioDto.Apellidos;
            usuario.Email = usuarioDto.Email;
            usuario.Imagen = usuarioDto.Imagen;
            var resultado = await _userManager.UpdateAsync(usuario);

            if (!resultado.Succeeded)
            {

                return BadRequest(new CodeErrorResponse(400, "No se pudo actualizar el usuario"));

            }
            else
            {
                return Ok("Todo bien");
            }
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<UsuarioDto>>> GetUsuarios([FromQuery] UsuarioSpecificationParams usuarioParams)
        {
            var spec = new UsuarioSpecification(usuarioParams);
            var usuarios = await _seguridadRepository.GetAllWithSpec(spec);

            var specCount = new UsuarioForCountingSpecification(usuarioParams);
            var totalUsuarios = await _seguridadRepository.CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalUsuarios) / Convert.ToDecimal( usuarioParams.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Usuario>, IReadOnlyList<UsuarioDto>>(usuarios);

            return Ok(
                new Pagination<UsuarioDto>
                {
                    Count = totalUsuarios,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = usuarioParams.PageIndex,
                    PageSize = usuarioParams.PageSize
                }
                );
                
        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(User);

            return new UsuarioDto
            {
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Username = usuario.UserName,
                //Token = _tokenServices.CreateToken(usuario)
            };

        }

        [HttpGet("emailvalid")]

        public async Task<ActionResult<bool>> ValidarEmail([FromQuery] string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;
            return true;
        }

        [Authorize]
        [HttpGet("direccion")]

        public async Task<ActionResult<DireccionDto>> GetDireccion()
        {
            //var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usuario = await _userManager.BuscarDireccionUsuarioAsync(User);

            return _mapper.Map<Direccion, DireccionDto>(usuario.Direccion);

        }

        [Authorize]
        [HttpPut("direccion/editar")]

        public async Task<ActionResult<DireccionDto>> UpdateDireccion(DireccionDto nuevaDireccion)
        {
            var usuario = await _userManager.BuscarDireccionUsuarioAsync(User);

            usuario.Direccion = _mapper.Map<DireccionDto, Direccion>(nuevaDireccion);
            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded) return Ok(_mapper.Map<Direccion, DireccionDto>(usuario.Direccion));

            return BadRequest("No se pudo actualizar la dirección");
        }

        [Authorize]
        [HttpPut("role/{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateRole(string id, RoleDto roleParam)
        {
            var role = await _rolManager.FindByNameAsync(roleParam.NombreRole);
            if(role == null) return NotFound(new CodeErrorResponse(404, "El rol no exite"));

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound(new CodeErrorResponse(404, "El usuario no existe."));

            var usuarioDto = _mapper.Map<Usuario,UsuarioDto>(usuario);

            if (roleParam.Status)
            {
                var resultado = await _userManager.AddToRoleAsync(usuario, roleParam.NombreRole);
                if (resultado.Succeeded)
                {
                    usuarioDto.Administrador = true;
                }

                if (resultado.Errors.Any())
                {
                    if(resultado.Errors.Where(x=> x.Code == "UserAlreadyInRole").Any())
                    {
                        usuarioDto.Administrador = true;
                    }
                }

            }
            else
            {
                var resultado = await _userManager.RemoveFromRoleAsync(usuario, roleParam.NombreRole);

                if (resultado.Succeeded)
                {
                    usuarioDto.Administrador = false;
                }
            }

            return usuarioDto;
        }

    }
}
