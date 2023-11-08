using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenServices;
        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, 
            ITokenService tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UsuarioDto>> Login (LoginDto loginDto)
        {
            var usuario =  await _userManager.FindByEmailAsync(loginDto.Email);
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
                Token = _tokenServices.CreateToken(usuario),
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

            if(!resultado.Succeeded) 
            {
                return BadRequest(new CodeErrorResponse(400, "Error al registrar el usuario"));
            }

            return new UsuarioDto
            {
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Token = _tokenServices.CreateToken(usuario),
                Email = usuario.Email,
                Username = usuario.UserName,
                
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario() 
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var usuario = await _userManager.FindByEmailAsync(email);

            return new UsuarioDto
            {
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenServices.CreateToken(usuario)
            };

        }

        [HttpGet("emailvalid")]

        public async Task<ActionResult<bool>> ValidarEmail([FromQuery]string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;
            return true;
        }

        [Authorize]
        [HttpGet("direccion")]

        public async Task<ActionResult<Direccion>> GetDireccion()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var usuario = await _userManager.FindByEmailAsync(email);

            return usuario.Direccion;

        }
    }
}
