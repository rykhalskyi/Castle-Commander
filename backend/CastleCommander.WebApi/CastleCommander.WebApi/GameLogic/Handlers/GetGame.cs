using CastleCommander.WebApi.GameLogic.Enemies;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class GetGame
    {
        public class Query : IRequest<Game>
        {
            public Guid GameId { get; set; }
        }

        public class Handler(
            IGameFlow gameFlow,
            IGamesCache gamesCache,
            IEnemyCardsCache enemyCardsCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.GameId);
                return Task.FromResult(game);
            }
        }
    }
}
