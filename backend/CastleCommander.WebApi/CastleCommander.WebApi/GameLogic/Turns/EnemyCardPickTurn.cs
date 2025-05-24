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

    }
}
