using CastleCommander.WebApi.GameLogic.Enemies;

namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class FightTurn(IEnemyCardsCache enemyCardsCache) : BaseTurn
    {
        protected override string Message => "And fight!";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);

            if (game.CurrentCards.ImpactCard == null) return;

            if (enemyCardsCache.Cards[game.CurrentCards.ImpactCard.Index] is EnemyCard enemyCard)
            {
                var random = new Random();
                foreach (var hex in game.Castle.Hexagons.Where(i => i.Affected))
                {
                    for (int i = 0; i < enemyCard.SectorsNumber; i++) {
                        var index = random.Next(6);

                        hex.Sectors[index].ImpactValue = enemyCard.ImpactValue;//MakeImpactScore(random, enemyCard.ImpactValue);
                    }
                }
            } 
        }

        private int MakeImpactScore(Random random, int maxCardScore)
        {
            if (maxCardScore < 0)
            {
                return random.Next(Math.Abs(maxCardScore)) * -1;
            }
            return random.Next(maxCardScore);
        }
    }
}
