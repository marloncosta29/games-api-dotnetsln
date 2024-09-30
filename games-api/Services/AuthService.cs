using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GamesAPI.Context;
using GamesAPI.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GamesAPI.Services
{
    public class AuthService
    {
        private readonly GamesDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(GamesDbContext context, IConfiguration configuration){
            this._context = context;
            this._configuration = configuration;
        }

        public string Authenticate(string username, string password){
            UserEntity user = this._context.Users.SingleOrDefault(user => user.Username == username);
            if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.Hash)){
                return null;
            }
            // cria o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                ]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}