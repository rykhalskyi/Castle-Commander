namespace CastleCommander.WebApi.GameLogic
{
    public class Game 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TurnMessage { get; set; }
        public int CurrentTurn { get; set; }
    }
}
