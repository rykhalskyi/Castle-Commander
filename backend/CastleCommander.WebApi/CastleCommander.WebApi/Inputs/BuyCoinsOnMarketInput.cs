using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class BuyCoinsOnMarketInput : BaseInput
    {
        public int[] Resources { get; set; } = [];
        public ExchangeItem Item { get; set; }
    }
}
