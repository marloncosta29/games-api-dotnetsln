using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GamesAPI.Context
{
    public class GamesDbContextFactory : IDesignTimeDbContextFactory<GamesDbContext>
    {
        public GamesDbContext CreateDbContext(string[] args)
        {
            // Lê a configuração do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GamesDbContext>();

            // Configura o DbContext para usar o PostgreSQL
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new GamesDbContext(optionsBuilder.Options);
        }
    }
}