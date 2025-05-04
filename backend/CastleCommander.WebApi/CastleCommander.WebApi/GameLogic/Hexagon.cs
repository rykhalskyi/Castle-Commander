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

    public static class HexagonColors
    {
        public static readonly string[] Values = new string[]
        {
            "#eb0008", "#ff9d00", "#ffea00", "#00c403", "#00e0d5", "#171ae3", "#9600ed", "#695c00", "#d9d9d9", "#dbaf00"
        };
    }
    

    public class Hexagon
    {
        public string ColorValue { get; set; }
        public HexagonColor Color { get; set; }
        public List<Facility> Facilities { get; set; } = new();
    }
}
