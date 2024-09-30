

using GamesAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GamesApiTests
{
    public class DbContextHelper
    {

        public static GamesDbContext GetInMomoryDbContext(){
            var options = new DbContextOptionsBuilder<GamesDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new GamesDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }


    }
}