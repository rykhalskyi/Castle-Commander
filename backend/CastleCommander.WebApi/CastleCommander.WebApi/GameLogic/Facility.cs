namespace CastleCommander.WebApi.GameLogic
{
    public interface IFacility
    {
        public int Size { get; }
    }
    public class SmallFacility : IFacility
    {
        public int Size => 1;
    }

    public class MediumFacility : IFacility
    {
        public int Size => 2;
    }

    public class LargeFacility : IFacility
    {
        public int Size => 3;
    }
}
