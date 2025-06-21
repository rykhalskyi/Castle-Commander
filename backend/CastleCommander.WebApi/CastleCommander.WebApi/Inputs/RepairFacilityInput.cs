using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class RepairFacilityInput : BaseInput
    {
        public int Hexagon { get; set; }
        public int Sector { get; set; }
    }
}
