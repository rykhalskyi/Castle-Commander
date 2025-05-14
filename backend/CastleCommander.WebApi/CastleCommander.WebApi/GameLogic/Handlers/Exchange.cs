using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class Exchange
    {
        public class Query : IRequest<Game>
        {
            public Guid GameId { get; set; }
            public ExchangeItem Item { get; set; }
            public int Number { get; set; }
            
        }

        public class Handler(IGamesCache gamesCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.GameId);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                var player = game.Players[game.CurrentPlayer];

                Market.Exchange(request.Item, player);
                return Task.FromResult(game);
            }
        }
    }
}
