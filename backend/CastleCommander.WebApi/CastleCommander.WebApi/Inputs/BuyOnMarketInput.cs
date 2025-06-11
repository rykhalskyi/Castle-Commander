namespace CastleCommander.WebApi.Inputs
{
    public class BuyOnMarketInput
    {
        public Guid GameId { get; set; }
        public int ResourceToSell { get; set; }
        public int ResourceToBuy { get; set; }
    }
}
