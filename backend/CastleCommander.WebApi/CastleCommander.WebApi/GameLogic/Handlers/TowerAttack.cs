using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class TowerAttack
    {
        public class Query : IRequest<Game>
        {
            public Guid GameId { get; set; }
            public int Hexagon { get; set; }

            public int Player { get; set; }
        }

        public class Handler(IGamesCache cache) : IRequestHandler<Query, Game>
        {
            public async Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = cache.GetGame(request.GameId);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }

                var tower = game.Castle.Hexagons[request.Hexagon].Tower;
                if (tower == null || tower.PlayerId != request.Player)
                {
                    game.Log += "You have no tower in this hex \n";
                    return await Task.FromResult(game);
                }

                if (!Market.CanAttack(game.Players[request.Player], request.Hexagon))
                {
                    game.Log += "You have no enough bronye to attack \n";
                    return await Task.FromResult(game);
                }

                if (request.Hexagon == 0) 
                {
                    //Let's eliminate all threats if market attacks
                    for (int i=1; i<7; i++)
                        ClearImpactValue(i, game);
                }
                else
                {
                    ClearImpactValue(request.Hexagon, game);
                }


                return await Task.FromResult(game);
            }

            private static void ClearImpactValue(int hex, Game game)
            {
                foreach (var sector in game.Castle.Hexagons[hex].Sectors)
                {
                    sector.ImpactValue = 0;
                }
                game.Log += $"All threats in hexagon {hex} are eliminated \n";
            }
        }
    }
}
