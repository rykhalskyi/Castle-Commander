namespace CastleCommander.WebApi.GameLogic
{
    public class FacilityHelper
    {
        public static bool DoesFit(Hexagon hexagon, Facility facility)
        {
            switch (facility.Size)
            {
                case FacilitySize.Small:
                    return DoesFitSector(hexagon, facility.StartSector);
                case FacilitySize.Medium:
                    return DoesFitSector(hexagon, facility.StartSector) && 
                           DoesFitSector(hexagon, Offset(facility.StartSector, 1));
                case FacilitySize.Large:
                    return DoesFitSector(hexagon, facility.StartSector) &&
                          DoesFitSector(hexagon, Offset(facility.StartSector, 1)) &&
                          DoesFitSector(hexagon, Offset(facility.StartSector, 2));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void ApplyFacilitiesToDefenceScore(Hexagon hexagon)
        {
            if (hexagon.Facilities.Count == 0)
            {
                foreach (var sector in hexagon.Sectors)
                {
                    sector.DefenceScore = 1;
                }
                return;
            }

            foreach (var facility in hexagon.Facilities)
            {
                switch (facility.Size)
                {
                    case FacilitySize.Small:
                        hexagon.Sectors[facility.StartSector-1].DefenceScore = 2;
                        break;
                    case FacilitySize.Medium:
                        hexagon.Sectors[facility.StartSector-1].DefenceScore = 3;
                        hexagon.Sectors[Offset(facility.StartSector, 1) - 1].DefenceScore = 3;
                        break;
                    case FacilitySize.Large:
                        hexagon.Sectors[facility.StartSector-1].DefenceScore = 4;
                        hexagon.Sectors[Offset(facility.StartSector, 1)-1].DefenceScore = 4;
                        hexagon.Sectors[Offset(facility.StartSector, 2)-1].DefenceScore = 4;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        private static bool DoesFitSector(Hexagon hexagon, int sector)
        {
            return !hexagon.Facilities.Any(f=> 
                f.StartSector == sector ||
                f.Size == FacilitySize.Medium && f.StartSector == Offset(sector, -1) ||
                f.Size == FacilitySize.Large && f.StartSector == Offset(sector, -2) 
            );
        }

        private static int Offset(int sector, int delta)
        {
            var absolute = sector + delta;
            if (absolute > 6)
            {
                return absolute - 6;
            }
            if (absolute < 1)
            {
                return absolute + 6;
            }
            return absolute;
        }
    }
}
