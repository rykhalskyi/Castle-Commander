using CastleCommander.WebApi.Inputs;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public interface IBaseGameRequest : IRequest<Game>
    {
        Guid GameId { get; set; }
        Guid PlayerId { get; set; }
        
    }

    public abstract class BaseGameRequest : IBaseGameRequest
    {
        public Guid GameId { get; set; } = Guid.Empty;
        public Guid PlayerId { get; set; } = Guid.Empty;
    }
}
