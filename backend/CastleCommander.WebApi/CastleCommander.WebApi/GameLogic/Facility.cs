namespace CastleCommander.WebApi.GameLogic
{
    public interface IFacility
    {
        public int Size { get; }
        public int StartSector { get; set; }
    }
    public class SmallFacility : IFacility
    {
        public int Size => 1;

        public int StartSector { get; set; }
    }

    public class MediumFacility : IFacility
    {
        public int Size => 2;
        public int StartSector { get; set; }
    }

    public class LargeFacility : IFacility
    {
        public int Size => 3;
        public int StartSector { get; set; }
    }
}
