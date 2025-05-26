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

            game.CurrentEnemyCardIndex = _enemyCards.GetNextCardIndex(game.Id);
            var enemyCard = _enemyCards.Cards[game.CurrentEnemyCardIndex];
            game.CurrentEnemyCardDescription = enemyCard.Description;

            ResetAffectedHexagons(game);

            if (enemyCard is EventCard eventCard)
            {

            }
            else SeAffectedHexagons(game, enemyCard);


        }



        private void SeAffectedHexagons(Game game, BaseEnemyCard baseCard)
        {
            var random = new Random();
            var sectorsNumber = 0;

            if (baseCard is EnemyCard enemyCard)
            {
                sectorsNumber = enemyCard.SectorsNumber;
            }
            else if (baseCard is BonusCard bonusCard)
            {
                sectorsNumber = bonusCard.SectorsNumber;
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
                foreach (var sector in hex.Facilities)
                {
                    sector.Affected = false;
                }
            }
        }
    }
}
