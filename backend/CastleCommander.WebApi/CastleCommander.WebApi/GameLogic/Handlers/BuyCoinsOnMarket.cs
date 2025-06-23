
namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class BuyCoinsOnMarket
    {
        public class Request : BaseGameRequest
        {
            public int[] Resources { get; set; } = [];
            public ExchangeItem Item { get; set; }
        }

        public class Handler(IGamesCache gamesCache, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCache, eventSender)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                if (!Market.BuyCoins(request.Resources, request.Item, Player))
                {
                    Game.Log += "Not enough resources to buy coins\n";
                };

                return Task.FromResult(Game);
            }
        }
    }
}
