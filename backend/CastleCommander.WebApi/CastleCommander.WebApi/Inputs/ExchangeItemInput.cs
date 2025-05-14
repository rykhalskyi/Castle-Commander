using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class ExchangeItemInput
    {
        public Guid GameId { get; set; }
        public ExchangeItem Item { get; set; }
        public int Number { get; set; }
    }
}
