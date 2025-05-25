namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public class CardDeck
    {
        private Stack<int> _cards;
        public CardDeck(int cardNumber)
        {
            var cards = MakeList(cardNumber);
            Shuffle(cards);
            _cards = new Stack<int>(cards);
        }

        public bool TryGetNextCard(out int nextCard)
        {
            if (_cards.Count == 0)
            {
                nextCard = -1;
                return false;
            }
            nextCard = _cards.Pop();
            return true;
        }

        private List<int> MakeList(int cardNumber)
        {
            var cards = new List<int>();
            for (int i = 0; i < cardNumber; i++)
            {
                cards.Add(i);
            }
            return cards;
        }

        private void Shuffle(List<int> list)
        {
            var random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]); 
            }
        }
    }
}
