using GamesAPI.Context;
using GamesAPI.DTO;
using GamesAPI.Entities;
using GamesAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controller{
    [ApiController]
    [Route("games")]
    public class GameController(GamesDbContext context) : ControllerBase {

        private readonly GamesDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameRequestDto data){
            GameEntity newGame = new() {Name = data.Name, Tipo = data.Tipo, Descricao = data.Descricao};

            this._context.Games.Add(newGame);
            await this._context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newGame.Id }, newGame);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            List<GameEntity> games = await this._context.Games.ToListAsync();
            return Ok(games);

        }    

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(Guid id){
            GameEntity game = await this._context.Games.FindAsync(id);

            if(game == null){
                return NotFound();
            }
            return Ok(game);

        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateInfo(Guid id, UpdateGameRequestDto data){
            GameEntity game = await this._context.Games.FindAsync(id);

            if(game == null){
                return NotFound();
            }
            game.Patch(data);

            this._context.Games.Update(game);
            await this._context.SaveChangesAsync();
            return Ok(game);
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(Guid id){
            GameEntity game = await this._context.Games.FindAsync(id);

            if(game == null){
                return NotFound();
            }
            
            this._context.Games.Remove(game);
            await this._context.SaveChangesAsync();
            return NoContent();
        }
    }

}