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
            CheckEventCards(game);
        }

        private void CollectResources(Game game)
        {
            for (int i = 1; i < game.Castle.Hexagons.Count; i++)
            {
                foreach (var facility in game.Castle.Hexagons[i].Facilities)
                {
                    var resourceFromFacility = GetResourceFromFacility(facility);

                    var earnedResource = (game.PlayerId == facility.PlayerId)
                        ? ApplyBounus(resourceFromFacility, game.Dice?.BonusDice)
                        : 0;//resourceFromFacility;

                    var player = game.GetPlayer(facility.PlayerId).Resources[i - 1].Number += GetResourceFromFacility(facility);
                }
            }

            if (game.Dice == null) return;

            //Bonus resource
            game.Players[game.CurrentPlayer].Resources[game.Dice.ResorceDice - 1].Number += ApplyBounus(1, game.Dice.BonusDice);
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

        private int ApplyBounus(int resource, string? bonus)
        {
            switch (bonus)
            {
                case "+1":
                    return resource + 1;
                case "+2":
                    return resource + 2;
                case "x2":
                    return resource * 2;
                case "x1":
                default:
                    return resource;
            }
        }

        private void CheckEventCards(Game game)
        {
            foreach (var card in game.CurrentCards.EventCards.ToList())
            {
                card.Value--;
                if (card.Value == 0)
                {
                    game.CurrentCards.EventCards.Remove(card);
                }
            }
        }
    }
}
