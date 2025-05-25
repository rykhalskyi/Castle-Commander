namespace CastleCommander.WebApi.GameLogic
{
    public class Game 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TurnMessage { get; set; } = string.Empty;
        public int CurrentTurn { get; set; }
        public int CurrentPlayer { get; set; } = 0;

        public List<Player> Players { get; set; } = new();

        public Castle Castle { get; set; }
        public Dice? Dice { get; set; } = null;

        public string? Log { get; set; }
        public int CurrentEnemyCardIndex { get; set; }
        public string CurrentEnemyCardDescription { get; set; } = String.Empty;
    }
}
