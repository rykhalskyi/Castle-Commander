
namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public interface IEnemyCardsCache
    {
        List<BaseEnemyCard> Deck { get; }
    }
}