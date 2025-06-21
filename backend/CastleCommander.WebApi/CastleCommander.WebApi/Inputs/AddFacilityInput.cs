using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class AddFacilityInput : BaseInput
    {
        public int Hexagon { get; set; }
        public int StartSector { get; set; }
        public FacilitySize Size { get; set; }
    }
}
