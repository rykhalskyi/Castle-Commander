using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class BuyOnMarket
    {
        public class Request : BaseGameRequest
        {
            public int ResourceToSell { get; set; }
            public int ResourceToBuy { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : BaseGameHandler<Request>(gamesCache)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                var ratio = Game.Castle.Hexagons[0]
                                .Facilities
                                .Where(i => i.PlayerId == Game.PlayerId)
                                .Sum(i => (int)i.Size);

                if (!Market.BuyResources(Player, request.ResourceToSell, request.ResourceToBuy, ratio))
                {
                    Game.Log = "Not enough resources to buy";
                }
                return Task.FromResult(Game);
            }
        }
    }
}
