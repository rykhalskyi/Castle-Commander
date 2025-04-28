
namespace CastleCommander.WebApi.GameLogic
{
    public interface IGamesCache
    {
        void AddGame(Game game);
        Game GetGame(Guid gameId);
    }
}