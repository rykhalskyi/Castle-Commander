using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class BuyCoinsOnMarket
    {
        public class Query : IRequest<Game>
        {
            public BuyCoinsOnMarketInput Input { get; set; }
        }

        public class Handler : IRequestHandler<Query, Game>
        {
            private readonly IGamesCache _gamesCache;
            public Handler(IGamesCache gamesCache)
            {
                _gamesCache = gamesCache;
            }
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = _gamesCache.GetGame(request.Input.GameId);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                var player = game.Players[game.CurrentPlayer];
                if (!Market.BuyCoins(request.Input.Resources, request.Input.Item, player))
                {
                    game.Log += "Not enough resources to buy coins\n";
                };

                return Task.FromResult(game);
            }
        }
    }
}
