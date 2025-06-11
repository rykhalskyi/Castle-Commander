using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class AddFacility
    {
        public class Query: IRequest<Game>
        {
            public Game InputGame { get; set; }
            public int Hexagon { get; set; }
            public int StartSector { get; set; }
            public FacilitySize Size { get; set; }
            public int PlayerId { get; set; }
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

                if (request.StartSector < 6)
                {
                    return AddFacility(request, game);
                }
                else
                {
                    return AddTower(request, game);
                }
            }

            private Task<Game> AddFacility(Query request, Game game)
            {
                var newFacility = new Facility()
                {
                    StartSector = request.StartSector,
                    Size = request.Size,
                    PlayerId = request.PlayerId,
                    PrimaryColor = request.InputGame.Players[request.PlayerId].PrimaryColor,
                    SecondaryColor = request.InputGame.Players[request.PlayerId].SecondaryColor
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

            private Task<Game> AddTower(Query request, Game game)
            {

                if (game.Castle.Hexagons[request.Hexagon].Tower != null)
                {
                    game.Log = "Castle already exists";
                    return Task.FromResult(game);
                }

                //if (!Market.TryBuildTower(game, request.Hexagon))
                //{
                //    game.Log = "Not enough resources to build facility";
                //    return Task.FromResult(game);
                //}

                var newTower = new Tower
                {
                    PlayerId = request.PlayerId,
                    PrimaryColor = request.InputGame.Players[request.PlayerId].PrimaryColor,
                    SecondaryColor = request.InputGame.Players[request.PlayerId].SecondaryColor
                };

                game.Castle.Hexagons[request.Hexagon].Tower = newTower;
                return Task.FromResult(game);
            }

        }
    }
}
