using CastleCommander.WebApi.GameLogic.Enemies;

namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class EnemyCardPickTurn : BaseTurn
    {
        private readonly IEnemyCardsCache _enemyCards;

        public EnemyCardPickTurn(IEnemyCardsCache enemyCards)
        {
            _enemyCards = enemyCards;
        }
        protected override string Message => "Enemy card pick...";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);

            var enemyCardIndex = _enemyCards.GetNextCardIndex(game.Id);
            var enemyCard = _enemyCards.Cards[enemyCardIndex];

            ResetAffectedHexagons(game);

            while (enemyCard is EventCard eventCard)
            {
                game.CurrentCards.EventCards.Add(new CurrentCards.CardDto
                {
                    Index = enemyCardIndex,
                    Description = eventCard.Description,
                    Value = eventCard.Duration,
                });

                enemyCardIndex = _enemyCards.GetNextCardIndex(game.Id);
                enemyCard = _enemyCards.Cards[enemyCardIndex];
            }

            game.CurrentCards.ImpactCard = new CurrentCards.CardDto
            {
                Index = enemyCardIndex,
                Description = enemyCard.Description,
                Value = (enemyCard as EnemyCard)?.ImpactValue ?? 0
            };

            SeAffectedHexagons(game, enemyCard);

        }


        private void SeAffectedHexagons(Game game, BaseEnemyCard baseCard)
        {
            var random = new Random();
            var sectorsNumber = 0;

            if (baseCard is EnemyCard enemyCard)
            {
                sectorsNumber = enemyCard.SectorsNumber;
            }

            for (int i = 0; i < sectorsNumber; i++)
            {
                var hexIndex = random.Next(1, game.Castle.Hexagons.Count);
                var hex = game.Castle.Hexagons[hexIndex];
                hex.Affected = true;
            }
        }

        private void ResetAffectedHexagons(Game game)
        {
            foreach (var hex in game.Castle.Hexagons)
            {
                hex.Affected = false;
                foreach (var sector in hex.Sectors)
                {
                    sector.ImpactValue = 0;
                }
            }
        }
    }
}
