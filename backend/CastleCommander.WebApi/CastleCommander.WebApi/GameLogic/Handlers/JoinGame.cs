using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class JoinGame
    {
        public class Request : BaseGameRequest
        {
        }

        public class Handler(IGamesCache gamesCache) : IRequestHandler<Request, Game>
        {
            public Task<Game> Handle(Request request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.GameId);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                PlayerCreator.AddPlayer(game);
                return Task.FromResult(game);
            }
        }
    }
}
