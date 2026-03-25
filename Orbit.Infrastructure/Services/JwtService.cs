using Orbit.Application.Interfaces;
using Orbit.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Orbit.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(ApplicationUsers user, List<string> roles)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,          user.UserName),
                new Claim(ClaimTypes.Email,          user.Email),
            };

            // ✅ Each role becomes a separate claim
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var expiry = DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpiryInMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}