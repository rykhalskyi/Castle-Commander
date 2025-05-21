namespace CastleCommander.WebApi.Inputs
{
    public class ExchangeItemInput
    {
        public Guid GameId { get; set; }
        public int OtherPlayer { get; set; }
        public int PlayerResource { get; set; }
        public int OtherResource { get; set; }
    }
}
