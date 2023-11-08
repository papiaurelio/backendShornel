using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class TokenServices : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nombres),
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Apellidos),
                new Claim("username", usuario.UserName)
            };

            var credencials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencials,
                Issuer = _configuration["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
