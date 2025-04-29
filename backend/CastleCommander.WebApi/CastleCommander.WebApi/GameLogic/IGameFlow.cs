
namespace CastleCommander.WebApi.GameLogic
{
    public interface IGameFlow
    {
        IList<IGameTurn> Turns { get; set; }

        Game NextTurn(Game userInput, Game game);
        Game StartGame(Game userInput);
    }
}