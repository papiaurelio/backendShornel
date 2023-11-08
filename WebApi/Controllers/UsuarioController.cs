using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                Token = "Token",
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos 
            };

        }

    }
}
