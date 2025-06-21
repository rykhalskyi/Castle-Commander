using CastleCommander.WebApi.Inputs;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class RepairFacility
    {
        public class Request: BaseGameRequest
        {
            public int Hexagon { get; set; }
            public int Sector { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : BaseGameHandler<Request>(gamesCache)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {

                if (Market.TryRepairFacility(Game, request.Hexagon))
                {
                    Game.Castle.Hexagons[request.Hexagon].Sectors[request.Sector].DefenceScore++;
                }
                else
                {
                    Game.Log = "Not enough resources to repair facility";
                }


                return Task.FromResult(Game);
            }
        }
    }
}
