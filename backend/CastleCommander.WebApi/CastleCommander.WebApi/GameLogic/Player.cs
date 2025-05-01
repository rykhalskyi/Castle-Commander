namespace CastleCommander.WebApi.GameLogic
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public PlayerResource[] Resources { get; set; } = new PlayerResource[6];
        public int Bronze { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
    }
}
