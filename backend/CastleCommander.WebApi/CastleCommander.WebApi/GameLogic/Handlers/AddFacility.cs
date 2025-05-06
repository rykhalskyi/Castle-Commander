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

                switch (request.Size)
                {
                    case FacilitySize.Small:
                    case FacilitySize.Medium:
                    case FacilitySize.Large:
                        game.Castle.Hexagons[request.Hexagon].Facilities.Add(new Facility()
                        {
                            StartSector = request.StartSector,
                            Size = request.Size
                        });
                        break;
                    default:
                        throw new Exception("Invalid facility size");
                }
               
                return Task.FromResult(game);
            }
        }
    }
}
