namespace CastleCommander.WebApi.GameLogic
{
    public interface IGameTurn
    {
        void UpdateGame(Game userInput, Game game);
        bool CanTurn(Game game);
        void MakeTurn(Game userInput, Game game);
    }
}
