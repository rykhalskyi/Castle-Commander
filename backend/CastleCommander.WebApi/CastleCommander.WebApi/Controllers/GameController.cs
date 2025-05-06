using CastleCommander.WebApi.GameLogic;
using CastleCommander.WebApi.GameLogic.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastleCommander.WebApi.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("startnew")]
        public async Task<Game> StartNewGame()
        {
            var game = await mediator.Send(new StartNewGame.Query() { });
            return game;
        }

        [AllowAnonymous]
        [HttpPost("nextturn")]
        public async Task<Game> NextTurn(Game inputGame)
        {
            var game = await mediator.Send(new NextTurn.Query() { InputGame = inputGame});
            return game;
        }

        [AllowAnonymous]
        [HttpPost("addfacility")]
        public async Task<Game> AddFacility(Game inputGame, int hexagon, int startSector, FacilitySize size)
        {
            var game = await mediator.Send(new AddFacility.Query() { 
                InputGame = inputGame,
                Hexagon = hexagon,
                StartSector = startSector,
                Size = size
            });
            return game;
        }
    }
}
