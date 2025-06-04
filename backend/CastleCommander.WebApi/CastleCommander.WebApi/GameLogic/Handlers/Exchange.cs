using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class Exchange
    {
        public class Query : IRequest<Game>
        {
            public ExchangeItemInput Input { get; set; }
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

                try
                {
                    var player = game.Players[game.CurrentPlayer];
                    var otherPlayer = game.Players[request.Input.OtherPlayer];

                    var success = Market.Exchange(player, otherPlayer, request.Input.PlayerResource, request.Input.OtherResource);

                    if (!success)
                    {
                        game.Log = "Not enough resources to exchange";
                    }
                }
                catch (Exception ex)
                {
                    game.Log = ex.Message;
                }

                return Task.FromResult(game);
            }
        }
    }
}
