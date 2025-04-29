namespace CastleCommander.WebApi.GameLogic.Turns
{
    public abstract class BaseTurn : IGameTurn
    {
        protected abstract string Message { get; }
        public virtual bool CanTurn(Game game)
        {
            return true;
        }

        public void MakeTurn(Game userInput, Game game)
        {
            game.TurnMessage = Message;
        }


        public void UpdateGame(Game userInput, Game game)
        {
        }
    }
}
