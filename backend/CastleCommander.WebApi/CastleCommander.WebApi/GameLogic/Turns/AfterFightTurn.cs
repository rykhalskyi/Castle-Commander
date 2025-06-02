namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class AfterFightTurn : BaseTurn
    {
        protected override string Message => "The Fight is over.";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);

            foreach (var hex in game.Castle.Hexagons)
                foreach (var sector in hex.Sectors)
                {
                    sector.DefenceScore += sector.ImpactValue;
                    sector.ImpactValue = 0;
                }
        }
    }
}
