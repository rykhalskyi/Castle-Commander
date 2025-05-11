namespace CastleCommander.WebApi.GameLogic
{
    public static class DiceHelper
    {
        private const int DiceSidesNumber = 6;
        
        public static Dice Throw()
        {
            var bonuses = new[] { "x1", "x1", "x2", "+1", "+1", "+2" };

            var random = new Random();
            var resourceDiceThrow = random.Next(DiceSidesNumber);
            var bonusDiceThrow = random.Next(DiceSidesNumber);

            return new Dice
            {
                ResorceDice = resourceDiceThrow + 1,
                ResourceDiceColor = HexagonColors.Values[resourceDiceThrow + 1],
                BonusDice = bonuses[bonusDiceThrow]
            };
        }
    }
}
