namespace CastleCommander.WebApi.GameLogic
{
    public class Game 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TurnMessage { get; set; }
        public int CurrentTurn { get; set; }
        public int CurrentPlayer { get; set; } = 0;

        public List<Player> Players { get; set; } = new();

        public Castle Castle { get; set; }
}
}
