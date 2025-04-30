namespace CastleCommander.WebApi.GameLogic
{
    public enum HexagonColor
    {
        Red = 0,
        Orange = 1,
        Yellow = 2,
        Green = 3,
        Blue = 4,
        Navy = 5,
        Purple = 6,
    }

    public class Hexagon
    {
        public HexagonColor Color { get; set; }
        public List<IFacility> Facilities { get; set; } = new();
    }
}
