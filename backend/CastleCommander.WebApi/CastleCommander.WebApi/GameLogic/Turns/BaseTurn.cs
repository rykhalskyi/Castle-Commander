namespace CastleCommander.WebApi.GameLogic.Turns
{
    public abstract class BaseTurn : IGameTurn
    {
        protected abstract string Message { get; }
        public virtual bool CanTurn(Game game)
        {
            return true;
        }

        public virtual void MakeTurn(Game userInput, Game game)
        {
            game.TurnMessage = Message;
        }


        public virtual void UpdateGame(Game userInput, Game game)
        {
        }
    }
}
