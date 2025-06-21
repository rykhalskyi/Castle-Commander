using CastleCommander.WebApi.Inputs;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class Exchange
    {
        public class Request : BaseGameRequest
        {
            public Guid OtherPlayer { get; set; }
            public int PlayerResource { get; set; }
            public int OtherResource { get; set; }
        }

        public class Handler(IGamesCache gamesCache) : BaseGameHandler<Request>(gamesCache)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var otherPlayer = Game.GetPlayer(request.OtherPlayer);
                    var success = Market.Exchange(Player, otherPlayer, request.PlayerResource, request.OtherResource);

                    if (!success)
                    {
                        Game.Log = "Not enough resources to exchange";
                    }
                }
                catch (Exception ex)
                {
                    Game.Log = ex.Message;
                }

                return Task.FromResult(Game);
            }
        }
    }
}
