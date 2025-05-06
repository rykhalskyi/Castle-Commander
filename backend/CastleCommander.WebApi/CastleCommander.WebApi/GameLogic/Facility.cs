namespace CastleCommander.WebApi.GameLogic
{
    public enum FacilitySize
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public class Facility
    {
        public FacilitySize Size { get; set; }

        public int StartSector { get; set; }
    }

    
}
