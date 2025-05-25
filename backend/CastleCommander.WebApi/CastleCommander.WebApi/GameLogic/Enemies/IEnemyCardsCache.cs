
namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public interface IEnemyCardsCache
    {
        List<BaseEnemyCard> Cards { get; }
        void AddDeck(Guid gameId);
        int GetNextCardIndex(Guid gameId);
    }
}