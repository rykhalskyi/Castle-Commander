using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class NextTurn
    {
        public class Query : IRequest<Game>
        {
            public Game InputGame { get; set; }
        }

        public class Handler(IGamesCache gamesCache, IGameFlow gameFlow) : IRequestHandler<Query, Game>
        {
            
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.InputGame.Id);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                return Task.FromResult(gameFlow.NextTurn(request.InputGame, game));

            }
        }

    }
}
