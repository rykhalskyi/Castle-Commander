using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class BuyOnMarket
    {
        public class Query : IRequest<Game>
        {
            public BuyOnMarketInput Input { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.Input.GameId);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                var player = game.Players[game.CurrentPlayer];
                var ratio = game.Castle.Hexagons[0]
                                .Facilities
                                .Where(i => i.PlayerId == game.CurrentPlayer)
                                .Sum(i => (int)i.Size);

                if (!Market.BuyResources(player, request.Input.ResourceToSell, request.Input.ResourceToBuy, ratio))
                {
                    game.Log = "Not enough resources to buy";
                }
                return Task.FromResult(game);
            }
        }
    }
}
