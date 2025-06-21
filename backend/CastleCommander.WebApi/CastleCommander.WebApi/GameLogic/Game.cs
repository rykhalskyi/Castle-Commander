using CastleCommander.WebApi.GameLogic.Enemies;

namespace CastleCommander.WebApi.GameLogic
{
    public class Game 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PlayerId { get; set; } = Guid.Empty;

        public string TurnMessage { get; set; } = string.Empty;
        public int CurrentTurn { get; set; }
        public int CurrentPlayer { get; set; }

        public List<Player> Players { get; set; } = new();

        public Castle Castle { get; set; }
        public Dice? Dice { get; set; } = null;

        public string? Log { get; set; }
        public CurrentCards CurrentCards { get; set; } = new();
    }
}
