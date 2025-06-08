using CastleCommander.WebApi.GameLogic;

namespace CastleCommander.WebApi.Inputs
{
    public class RepairFacilityInput
    {
        public Game InputGame { get; set; }
        public int Hexagon { get; set; }
        public int Sector { get; set; }
    }
}
