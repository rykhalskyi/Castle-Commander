using CastleCommander.WebApi.GameLogic;
using CastleCommander.WebApi.GameLogic.Handlers;
using CastleCommander.WebApi.Inputs;
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
        [HttpGet("getgame/{id}")]
        public async Task<Game> GetGame(Guid id)
        {
            var game = await mediator.Send(new GetGame.Query() { GameId = id });
            return game;
        }

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
        public async Task<Game> AddFacility(AddFacilityInput input)
        {
            var game = await mediator.Send(new AddFacility.Query() { 
                InputGame = input.InputGame,
                Hexagon = input.Hexagon,
                StartSector = input.StartSector,
                Size = input.Size,
                PlayerId = input.PlayerId
            });
            return game;
        }

        [AllowAnonymous]
        [HttpPost("buy")]
        public async Task<Game> BuyItems(BuyItemInput input)
        {
            return await mediator.Send(new Buy.Query
            {
                GameId = input.GameId,
                Item = input.Item
            });
        }

        [AllowAnonymous]
        [HttpPost("exchange")]
        public async Task<Game> ExchangeItems(ExchangeItemInput input)
        {
            return await mediator.Send(new Exchange.Query
            {
                Input = input
            });
        }

        [AllowAnonymous]
        [HttpPost("repairfacility")]
        public async Task<Game> RepairCastle(RepairFacilityInput input)
        {
            return await mediator.Send(new RepairFacility.Query
            {
                Input = input,
            });
        }

        [AllowAnonymous]
        [HttpPost("buyonmarket")]
        public async Task<Game> BuyItemsOnMarket(BuyOnMarketInput input)
        {
            return await mediator.Send(new BuyOnMarket.Query
            {
                Input = input
            });
        }

        [AllowAnonymous]
        [HttpPost("towerattack")]
        public async Task<Game> TowerAttack(Guid GameId, int hex)
        {
            return await mediator.Send(new TowerAttack.Query
            {
                GameId = GameId,
                Hexagon = hex
            });
        }

        [AllowAnonymous]
        [HttpPost("buycoinsonmarket")]
        public async Task<Game> BuyCoinsOnMarket(BuyCoinsOnMarketInput input)
        {
            return await mediator.Send(new BuyCoinsOnMarket.Query
            {
                Input = input,
            });
        }
    }
}
