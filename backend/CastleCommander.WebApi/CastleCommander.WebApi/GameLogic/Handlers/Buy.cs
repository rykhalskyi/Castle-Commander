using CastleCommander.WebApi.Inputs;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class Buy
    {
        public class Request : BaseGameRequest
        {
            public ExchangeItem Item { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : BaseGameHandler<Request>(gamesCache)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                Market.Buy(request.Item, Player);
                return Task.FromResult(Game);
            }
        }
    }
}
