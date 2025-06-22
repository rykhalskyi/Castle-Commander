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
        public async Task<Game> GetGame(Guid id, Guid playerId)
        {
            var game = await mediator.Send(new GetGame.Request() { 
                GameId = id,
                PlayerId = playerId
            });
            return game;
        }

        [AllowAnonymous]
        [HttpGet("join/{id}")]
        public async Task<Game> JoinGame(Guid id)
        {
            var game = await mediator.Send(new JoinGame.Request() { GameId = id });
            return game;
        }

        [AllowAnonymous]
        [HttpGet("startnew")]
        public async Task<Game> StartNewGame()
        {
            var game = await mediator.Send(new StartNewGame.Request() {  });
            return game;
        }

        [AllowAnonymous]
        [HttpPost("nextturn")]
        public async Task<Game> NextTurn(Game inputGame)
        {
            var game = await mediator.Send(new NextTurn.Request() {
                GameId = inputGame.Id,
                PlayerId = inputGame.PlayerId,
                InputGame = inputGame
            });
            return game;
        }

        [AllowAnonymous]
        [HttpPost("addfacility")]
        public async Task<Game> AddFacility(AddFacilityInput input)
        {
            var game = await mediator.Send(new AddFacility.Request() { 
                Hexagon = input.Hexagon,
                StartSector = input.StartSector,
                PlayerId = input.PlayerId,
                GameId = input.GameId,
                Size = input.Size
            });
            return game;
        }

        [AllowAnonymous]
        [HttpPost("buy")]
        public async Task<Game> BuyItems(BuyItemInput input)
        {
            return await mediator.Send(new Buy.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                Item = input.Item  
            });
        }

        [AllowAnonymous]
        [HttpPost("exchange")]
        public async Task<Game> ExchangeItems(ExchangeItemInput input)
        {
            return await mediator.Send(new Exchange.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                OtherPlayer = input.OtherPlayer,
                OtherResource = input.OtherResource,
                PlayerResource = input.PlayerResource
            });
        }

        [AllowAnonymous]
        [HttpPost("repairfacility")]
        public async Task<Game> RepairCastle(RepairFacilityInput input)
        {
            return await mediator.Send(new RepairFacility.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                Hexagon = input.Hexagon,
                Sector = input.Sector
            });
        }

        [AllowAnonymous]
        [HttpPost("buyonmarket")]
        public async Task<Game> BuyItemsOnMarket(BuyOnMarketInput input)
        {
            return await mediator.Send(new BuyOnMarket.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                ResourceToBuy = input.ResourceToBuy,
                ResourceToSell = input.ResourceToSell
            });
        }

        [AllowAnonymous]
        [HttpPost("towerattack")]
        public async Task<Game> TowerAttack(TowerAttackInput input)
        {
            return await mediator.Send(new TowerAttack.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                Hexagon = input.Hexagon
            });
        }

        [AllowAnonymous]
        [HttpPost("buycoinsonmarket")]
        public async Task<Game> BuyCoinsOnMarket(BuyCoinsOnMarketInput input)
        {
            return await mediator.Send(new BuyCoinsOnMarket.Request
            {
                GameId = input.GameId,
                PlayerId = input.PlayerId,
                Item = input.Item,
                Resources = input.Resources
            });
        }

        [AllowAnonymous]
        [HttpGet("events/{gameId}")]
        public async Task Events(Guid gameId)
        {
            Response.Headers.Append("Content-Type", "text/event-stream");

            var eventSender = HttpContext.RequestServices.GetRequiredService<IGameEventSender>() as SseGameEventSender;
            eventSender?.RegisterClient(gameId, Response);

            // Keep the connection open
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
    }
}
