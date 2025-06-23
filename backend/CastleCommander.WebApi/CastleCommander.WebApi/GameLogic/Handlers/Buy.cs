using CastleCommander.WebApi.Inputs;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class Buy
    {
        public class Request : BaseGameRequest
        {
            public ExchangeItem Item { get; set; }
        }

        public class Handler(IGamesCache gamesCache, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCache, eventSender)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                Market.Buy(request.Item, Player);
                return Task.FromResult(Game);
            }
        }
    }
}
