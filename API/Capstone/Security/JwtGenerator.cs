using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Capstone.Security
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string JwtSecret;

        public JwtGenerator(string secret)
        {
            JwtSecret = secret;
        }

        public string GenerateToken(int userId, string username)
        {
            return GenerateToken(userId, username, string.Empty);
        }

        public string GenerateToken(int userId, string username, string role)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("sub", userId.ToString()),
                new Claim("name", username),
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecret)), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
