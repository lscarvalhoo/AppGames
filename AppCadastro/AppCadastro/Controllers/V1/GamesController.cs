using AppCadastro.Exceptions;
using AppCadastro.InputModel;
using AppCadastro.Service;
using AppCadastro.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Controllers.V1
{
    [Route("api/V1/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1,
                                                                         [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);
            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        [HttpGet("{idGame : guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromRoute] Guid idGame)
        {
            var game = await _gameService.Get(idGame);
            if (game == null)
                return NoContent();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch (GameExistException ex)
            {
                return UnprocessableEntity(ex);
            }

        }

        [HttpPut("{idGame : guid}")]
        public async Task<ActionResult> RefreshGame([FromRoute] Guid idGame, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.Refresh(idGame, game);
                return Ok();
            }
            catch (GameNotExistException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPatch("{idGame : guid}/price/{price:double}")]
        public async Task<ActionResult> RefreshGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Refresh(idGame, price);
                return Ok();
            }
            catch (GameNotExistException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);
                return Ok();
            }
            catch (GameNotExistException ex)
            {
                return NotFound(ex);
            }
        }

    }
}
