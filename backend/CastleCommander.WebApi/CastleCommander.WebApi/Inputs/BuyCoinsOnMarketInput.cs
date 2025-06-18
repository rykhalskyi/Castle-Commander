using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class BuyCoinsOnMarketInput
    {
        public Guid GameId { get; set; }
        public int[] Resources { get; set; } = [];
        public ExchangeItem Item { get; set; }
    }
}
