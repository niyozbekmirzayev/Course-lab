using Courselab.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Courselab.Service.Auth
{
    public class AuthService : IAuthService
    {
        private IConfiguration config;

        public AuthService(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateToken(User user)
        {

            var claims = new List<Claim>
                {
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var issuer = config.GetSection("Jwt:Issuer").Value;
            var audience = config.GetSection("Jwt:Audience").Value;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                expires: DateTime.Now.AddMinutes(double.Parse(config.GetSection("JWT:Expire").Value)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
