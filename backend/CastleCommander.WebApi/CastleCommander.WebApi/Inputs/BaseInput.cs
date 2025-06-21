using System.Reflection.Metadata.Ecma335;

namespace CastleCommander.WebApi.Inputs
{
    public interface IBaseInput
    {
        Guid PlayerId { get; set; }
        Guid GameId { get; set; }
    }

    public class BaseInput : IBaseInput
    {
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }
    }
}
