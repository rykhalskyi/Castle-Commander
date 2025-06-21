namespace CastleCommander.WebApi.Inputs
{
    public class ExchangeItemInput : BaseInput
    {
        public Guid OtherPlayer { get; set; }
        public int PlayerResource { get; set; }
        public int OtherResource { get; set; }
    }
}
