using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class BuyItemInput : BaseInput
    {
        public ExchangeItem Item { get; set; }
        public int Number { get; set; }
    }
}
