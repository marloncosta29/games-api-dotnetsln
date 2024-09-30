using GamesAPI.Context;
using GamesAPI.DTO;
using GamesAPI.Entities;

namespace GamesAPI.Services
{
    public class UserService(GamesDbContext context) {
        private readonly GamesDbContext _context = context;
        
        public async Task<UserEntity> Create(CreateUserDto data){
            bool userAlreadyExists = this._context.Users.Any(user => user.Username == data.Username);
            if(userAlreadyExists){
                throw new Exception("Username \"" + data.Username + "\" is already taken");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(data.Password);

            UserEntity user = new(){
                Username = data.Username,
                Hash = passwordHash,
                Role = "USER"
            };

            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();
            return user;
        }

    }
}