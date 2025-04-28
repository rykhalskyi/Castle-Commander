using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class StartNewGame
    {
        public class Query : IRequest<Game>
        {
        }

        public class Handler(IGamesCache gamesCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = new Game
                {
                    Id = Guid.NewGuid(),
                };

                gamesCache.AddGame(game);

                return Task.FromResult(game);
            }
        }
    }
}
