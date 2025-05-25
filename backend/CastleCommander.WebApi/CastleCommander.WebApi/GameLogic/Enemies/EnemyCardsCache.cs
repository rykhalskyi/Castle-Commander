namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public class EnemyCardsCache : IEnemyCardsCache
    {
        public List<BaseEnemyCard> Cards { get; private set; }
        private Dictionary<Guid, CardDeck> _decks = new Dictionary<Guid, CardDeck>();

        public EnemyCardsCache(string cardFilePath)
        {
            Cards = Load(cardFilePath);
        }

        public void AddDeck(Guid gameId)
        {
            var newCardDeck = new CardDeck(Cards.Count);
            _decks.Add(gameId, newCardDeck);
        }

        public int GetNextCardIndex(Guid gameId)
        {
            if (_decks.TryGetValue(gameId, out var cardDeck))
            {
                if (cardDeck.TryGetNextCard(out var cardIndex))
                {
                    return cardIndex;
                }
                throw new KeyNotFoundException($"Card deck for a game with ID {gameId} is empty");
            }
            throw new KeyNotFoundException($"Card deck for a game with ID {gameId} not found in cache.");
        }

        private List<BaseEnemyCard> Load(string cardFilePath)
        {
            var file = File.ReadAllText(cardFilePath);
            var loader = new EnemyCardsLoader();
            return loader.Load(file);
        }
    }
}
