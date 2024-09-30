using GamesAPI.Controller;
using GamesAPI.DTO;
using GamesAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GamesApiTests {
    public class GamesControllerTests {
        [Fact]
        public async Task CreateGame_shouldCanCreate(){
            var context  = DbContextHelper.GetInMomoryDbContext();
            var controller = new GameController(context);
            CreateGameRequestDto newGame = new("My Game", "shooter", "my game description");
            
            var result = await controller.Create(newGame);

            var createdActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.GetById), createdActionResult.ActionName);

            var returnedGame = Assert.IsType<GameEntity>(createdActionResult.Value);
            Assert.IsType<Guid>(returnedGame.Id);
        }

    }

}