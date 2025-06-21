namespace CastleCommander.WebApi.Inputs
{
    public class BuyOnMarketInput : BaseInput
    {
        public int ResourceToSell { get; set; }
        public int ResourceToBuy { get; set; }
    }
}
