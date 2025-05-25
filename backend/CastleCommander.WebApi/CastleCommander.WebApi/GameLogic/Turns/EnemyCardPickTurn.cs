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
            game.CurrentEnemyCardDescription = _enemyCards.Cards[game.CurrentEnemyCardIndex].Description;
        }

    }
}
