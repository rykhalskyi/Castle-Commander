namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class TradeAndBuildTurn : BaseTurn
    {
        protected override string Message => "Trade and Build";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);
            game.Log = "";
        }

    }
}
