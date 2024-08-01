using Authentication.Library.AuthenticationManager.Contract;
using Authentication.Library.AuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Library.AuthenticationManager
{
    public class JwtAuthentication : IAuthenticationManager
    {
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        public JwtAuthentication(JwtTokenConfiguration jwtTokenConfiguration)
        {
            _jwtTokenConfiguration = jwtTokenConfiguration;
        }

        public string GenerateToken(string systemName)
        {

            var claims = new List<Claim>() 
            { 
                new Claim(JwtHeaderParameterNames.Jku, systemName), 
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()), 
                new Claim(ClaimTypes.NameIdentifier, systemName),
                new Claim(ClaimTypes.Role, "consumer")
            };

            var expires = DateTime.Now.AddMinutes(_jwtTokenConfiguration.AccessTokenExpiration);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfiguration.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtTokenConfiguration.Issuer, _jwtTokenConfiguration.Audience, claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtTokenConfiguration.Secret);

            try
            {
                var principle = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtTokenConfiguration.Issuer,
                    ValidAudience = _jwtTokenConfiguration.Audience,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validateToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
