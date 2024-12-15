using ChefMate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChefMate_backend.Services
{
    public class JwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<ChefMateRole> _roleManager;
        private readonly UserManager<ChefMateUser> _userManager;

        public JwtTokenService(
            IOptions<JwtSettings> jwtSettings,
            RoleManager<ChefMateRole> roleManager,
            UserManager<ChefMateUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(ChefMateUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id),
                new Claim("Email", user.Email),
                roles.Select(role => new Claim("Role", role)).FirstOrDefault()
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifeTimeMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
