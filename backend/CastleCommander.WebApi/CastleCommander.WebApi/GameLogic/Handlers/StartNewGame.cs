using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class StartNewGame
    {
        public class Query : IRequest<Game>
        {
            public Game InputGame { get; set; }
        }

        public class Handler(IGameFlow gameFlow, IGamesCache gamesCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var newGame = gameFlow.StartGame(request.InputGame);
                
                gamesCache.AddGame(newGame);

                return Task.FromResult(newGame);
            }
        }
    }
}
