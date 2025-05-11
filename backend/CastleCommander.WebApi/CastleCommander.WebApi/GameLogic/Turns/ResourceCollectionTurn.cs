namespace CastleCommander.WebApi.GameLogic.Turns
{
    public class ResourceCollectionTurn : BaseTurn
    {
        protected override string Message => "Collecting resources...";

        public override void MakeTurn(Game userInput, Game game)
        {
            base.MakeTurn(userInput, game);
            game.Dice = DiceHelper.Throw();
            CollectResources(game);
        }

        private void CollectResources(Game game)
        {
            for (int i = 1; i < game.Castle.Hexagons.Count; i++) {
                foreach (var facility in game.Castle.Hexagons[i].Facilities)
                {

                    game.Players[facility.PlayerId].Resources[i-1].Number += GetResourceFromFacility(facility);
                }
            }
        }

        private int GetResourceFromFacility(Facility facility)
        {
            switch (facility.Size)
            {
                case FacilitySize.Small:
                    return 1;
                case FacilitySize.Medium:
                    return 2;
                case FacilitySize.Large:
                    return 4;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
