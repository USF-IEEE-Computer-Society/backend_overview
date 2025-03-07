using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Workshop_Basics.Database;
using Workshop_Basics.Models;

namespace Workshop_Basics.Services
{
    public class JwtAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public JwtAuthService(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _appDbContext = dbContext;
        }

        public async Task<string?> GenerateTokenAsync(Credentials userCredential)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
            var securityKey = new SymmetricSecurityKey(key);

            var user = await _appDbContext.Users
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(u => u.Nickname == userCredential.nickname && u.Password == userCredential.password);

            if (user == null)
            {
                return null;
            }
            
            var userData = new UserData(user.FullName, user.UserId, user.Nickname, user.Email);
            bool isAdmin = user.Admin != null;

            var claims = new List<Claim>
            {
                new Claim("username", userData.nickname),
                new Claim(ClaimTypes.NameIdentifier, userData.userId.ToString()),
                new Claim(ClaimTypes.Email,userData.email),
                new Claim(ClaimTypes.GivenName, userData.nickname),
                new Claim("isAdmin", isAdmin? "adminFrFr":"justAnUser")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
