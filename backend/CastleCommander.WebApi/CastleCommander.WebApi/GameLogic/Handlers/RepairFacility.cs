using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class RepairFacility
    {
        public class Query: IRequest<Game>
        {
            public RepairFacilityInput Input { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : IRequestHandler<Query, Game>
        {
            public Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                var game = gamesCache.GetGame(request.Input.InputGame.Id);
                if (game == null)
                {
                    throw new Exception("Game not found");
                }
                var player = game.Players[game.CurrentPlayer];
                
                if (Market.TryRepairFacility(game, request.Input.Hexagon))
                {
                    game.Castle.Hexagons[request.Input.Hexagon].Sectors[request.Input.Sector].DefenceScore++;
                }
                else
                {
                    game.Log = "Not enough resources to repair facility";
                }


                return Task.FromResult(game);
            }
        }
    }
}
