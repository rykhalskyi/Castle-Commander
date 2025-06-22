using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class NextTurn
    {
        public class Request : BaseGameRequest
        {
            public Game InputGame { get; set; }
        }

        public class Handler(IGamesCache gamesCache, IGameFlow gameFlow, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCache)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                
                var result = Task.FromResult(gameFlow.NextTurn(request.InputGame, Game));
                eventSender.NextUpdate(Game.Id, request.PlayerId);
                return result;

            }
        }

    }
}
