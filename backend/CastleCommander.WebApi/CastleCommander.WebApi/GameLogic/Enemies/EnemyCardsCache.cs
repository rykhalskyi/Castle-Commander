namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public class EnemyCardsCache : IEnemyCardsCache
    {
        public List<BaseEnemyCard> Deck { get; private set; }
        public EnemyCardsCache(string cardFilePath)
        {
            Deck = Load(cardFilePath);
        }
        private List<BaseEnemyCard> Load(string cardFilePath)
        {
            var file = File.ReadAllText(cardFilePath);
            var loader = new EnemyCardsLoader();
            return loader.Load(file);
        }
    }
}
