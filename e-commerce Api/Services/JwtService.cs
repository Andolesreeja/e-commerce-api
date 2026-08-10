using e_commerce_Api.Data;
using e_commerce_Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce_Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public JwtService(
            IConfiguration configuration,
            AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GenerateToken(string username)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var issuedAt = DateTime.UtcNow;
            var expiresAt = issuedAt.AddHours(1);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: issuedAt,
                expires: expiresAt,
                signingCredentials: credentials
            );

            var tokenString =
                new JwtSecurityTokenHandler().WriteToken(token);

            // Save token information in database
            var tokenHistory = new TokenHistory
            {
                Username = username,
                Token = tokenString,
                IssuedAt = issuedAt,
                ExpiresAt = expiresAt
            };

            _context.TokenHistories.Add(tokenHistory);

            await _context.SaveChangesAsync();

            return tokenString;
        }
    }
}