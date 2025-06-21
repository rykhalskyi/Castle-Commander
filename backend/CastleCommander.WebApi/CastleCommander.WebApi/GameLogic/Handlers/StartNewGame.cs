using CastleCommander.WebApi.GameLogic.Enemies;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class StartNewGame
    {
        public class Request : IRequest<Game>
        {
        }

        public class Handler(
            IGameFlow gameFlow, 
            IGamesCache gamesCache, 
            IEnemyCardsCache enemyCardsCache) : IRequestHandler<Request, Game>
        {
            public Task<Game> Handle(Request request, CancellationToken cancellationToken)
            {
                var newGame = gameFlow.StartGame();
                
                gamesCache.AddGame(newGame);
                enemyCardsCache.AddDeck(newGame.Id);

                return Task.FromResult(newGame);
            }
        }
    }
}
