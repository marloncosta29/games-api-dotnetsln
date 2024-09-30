using Microsoft.EntityFrameworkCore;
using GamesAPI.Entities;

namespace GamesAPI.Context
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options) { }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}