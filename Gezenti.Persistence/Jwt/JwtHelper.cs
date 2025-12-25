using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gezenti.Application.Services;
using Gezenti.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gezenti.Persistence.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");

            string issuer = jwtSection["Issuer"]!;
            string audience = jwtSection["Audience"]!;
            string key = jwtSection["Key"]!;
            string expiresMinutesStr = jwtSection["ExpiresMinutes"] ?? "60";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // ✅ Script'e göre: UserName, UserGmail
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.UserGmail),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(expiresMinutesStr)),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
