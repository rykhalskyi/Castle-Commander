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

                var newFacility = new Facility()
                {
                    StartSector = request.StartSector,
                    Size = request.Size,
                    PlayerId = request.PlayerId,
                    PrimaryColor = request.InputGame.Players[request.PlayerId].PrimaryColor,
                    SecondaryColor = request.InputGame.Players[request.PlayerId].SecondaryColor
                };

                if ( FacilityHelper.DoesFit(game.Castle.Hexagons[request.Hexagon], newFacility))
                {
                    game.Castle.Hexagons[request.Hexagon].Facilities.Add(newFacility);
                }
               
                return Task.FromResult(game);
            }
        }
    }
}
