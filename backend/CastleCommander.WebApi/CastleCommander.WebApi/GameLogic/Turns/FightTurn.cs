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
                var eventCardImpactValue = EventCardImpactValue(game.CurrentCards.EventCards.Select(i => i.Index).ToList());
                var random = new Random();
                foreach (var hex in game.Castle.Hexagons.Where(i => i.Affected))
                {
                    game.Log += $"Hexagon {hex.Color} is affected by enemy card with impact value {enemyCard.ImpactValue} and event card impact value {eventCardImpactValue}.\n";
                    for (int i = 0; i < enemyCard.SectorsNumber; i++)
                    {
                        var index = random.Next(6);

                        hex.Sectors[index].ImpactValue = enemyCard.ImpactValue + eventCardImpactValue;//MakeImpactScore(random, enemyCard.ImpactValue);
                        game.Log += $"Sector {index}, value {hex.Sectors[index].ImpactValue}.\n";
                    }
                }

                game.Log += "Click on your Tower if exists to eliminate enemies of hex(1 Bronze) or all enemies (1 Gold for market tower)\n";
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

        private int EventCardImpactValue(List<int> cardIndexes)
        {
            var result = 0;
            foreach (var index in cardIndexes)
            {
                if (enemyCardsCache.Cards[index] is EventCard eventCard)
                {
                    result += eventCard.Defence - eventCard.EnemyAttack;
                }
            }
            return result;
        }
    }
}
