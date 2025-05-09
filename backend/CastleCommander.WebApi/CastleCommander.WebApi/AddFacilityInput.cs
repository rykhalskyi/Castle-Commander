using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi
{
    public class AddFacilityInput
    {
        public Game InputGame { get; set; }
        public int Hexagon { get; set; }
        public int StartSector { get; set; }
        public FacilitySize Size { get; set; }
        public int PlayerId { get; set; }
    }
}
