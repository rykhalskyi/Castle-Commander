namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class ResourceCollectionTurn : BaseTurn
    {
        protected override string Message => "Collecting resources...";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);
            game.Dice = DiceHelper.Throw();
        }

    }
}
