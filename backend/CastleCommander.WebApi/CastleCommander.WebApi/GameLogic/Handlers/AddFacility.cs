using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class AddFacility
    {
        public class Request: BaseGameRequest
        {
            public int Hexagon { get; set; }
            public int StartSector { get; set; }
            public FacilitySize Size { get; set; }
        }

        public class Handler(IGamesCache gamesCache, IGameFlow gameFlow, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCache, eventSender)
        {

            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                if (request.StartSector < 6)
                {
                    return AddFacility(request, Game);
                }
                else
                {
                    return AddTower(request, Game);
                }
            }

            private Task<Game> AddFacility(Request request, Game game)
            {
          
                var newFacility = new Facility()
                {
                    StartSector = request.StartSector,
                    Size = request.Size,
                    PlayerId = request.PlayerId,
                    PrimaryColor = Player.PrimaryColor,
                    SecondaryColor = Player.SecondaryColor
                };

                if (FacilityHelper.AreSectorsDestroyed(game.Castle.Hexagons[request.Hexagon], newFacility))
                {
                    game.Log = "One or many of selected sectors are destroyed";
                    return Task.FromResult(game);
                }

                if (FacilityHelper.DoesFit(game.Castle.Hexagons[request.Hexagon], newFacility) &&
                    Market.TryBuildFacility(game, request.Size, request.Hexagon))
                {
                    game.Castle.Hexagons[request.Hexagon].Facilities.Add(newFacility);
                    FacilityHelper.ApplyFacilitiesToDefenceScore(game.Castle.Hexagons[request.Hexagon], newFacility);
                }
                else
                {
                    game.Log = "Not enough resources or space to build facility";
                }

                return Task.FromResult(game);
            }

            private Task<Game> AddTower(Request request, Game game)
            {

                if (game.Castle.Hexagons[request.Hexagon].Tower != null)
                {
                    game.Log = "Castle already exists";
                    return Task.FromResult(game);
                }

                if (!Market.TryBuildTower(game, request.Hexagon))
                {
                    game.Log = "Not enough resources to build facility";
                    return Task.FromResult(game);
                }

                var newTower = new Tower
                {
                    PlayerId = Player.Id,
                    PrimaryColor = Player.PrimaryColor,
                    SecondaryColor = Player.SecondaryColor
                };

                game.Castle.Hexagons[request.Hexagon].Tower = newTower;
                return Task.FromResult(game);
            }

        }
    }
}
