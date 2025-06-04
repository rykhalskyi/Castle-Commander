namespace CastleCommander.WebApi.GameLogic
{
    public class FacilityHelper
    {
        private const int MinBuildableSectorScore = 0;
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

        public static void ApplyFacilitiesToDefenceScore(Hexagon hexagon, Facility facility)
        {
            if (hexagon.Facilities.Count == 0)
            {
                foreach (var sector in hexagon.Sectors)
                {
                    sector.DefenceScore = 1;
                }
                return;
            }

            switch (facility.Size)
            {
                    case FacilitySize.Small:
                        hexagon.Sectors[facility.StartSector].DefenceScore += 1;
                        break;
                    case FacilitySize.Medium:
                        hexagon.Sectors[facility.StartSector].DefenceScore += 2;
                        hexagon.Sectors[Offset(facility.StartSector, 1)].DefenceScore +=2;
                        break;
                    case FacilitySize.Large:
                        hexagon.Sectors[facility.StartSector].DefenceScore += 3;
                        hexagon.Sectors[Offset(facility.StartSector, 1)].DefenceScore += 3;
                        hexagon.Sectors[Offset(facility.StartSector, 2)].DefenceScore += 3;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
            }

        }

        public static bool AreSectorsDestroyed(Hexagon hex, Facility facility)
        {
            if (facility.Size == FacilitySize.Small)
            {
                return hex.Sectors[facility.StartSector].DefenceScore < MinBuildableSectorScore;
            }
            if (facility.Size == FacilitySize.Medium)
            {
                return hex.Sectors[facility.StartSector - 1].DefenceScore < MinBuildableSectorScore &&
                       hex.Sectors[Offset(facility.StartSector, 1)].DefenceScore < MinBuildableSectorScore;
            }
            if (facility.Size == FacilitySize.Large)
            {
                return hex.Sectors[facility.StartSector - 1].DefenceScore < MinBuildableSectorScore &&
                       hex.Sectors[Offset(facility.StartSector, 1)].DefenceScore < MinBuildableSectorScore &&
                       hex.Sectors[Offset(facility.StartSector, 2)].DefenceScore < MinBuildableSectorScore;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static void CheckAndDestroy(Hexagon hex, int sectorIndex)
        {
            if (hex.Sectors[sectorIndex].DefenceScore >= 0) return;

            var smallFacility = hex.Facilities.FirstOrDefault(f =>
                f.Size == FacilitySize.Small &&
                f.StartSector == sectorIndex);

            if (smallFacility != null)
            {
                hex.Facilities.Remove(smallFacility);
                return;
            }

            var mediumFacility = hex.Facilities.FirstOrDefault(f =>
                f.Size == FacilitySize.Medium &&
                (f.StartSector == sectorIndex || f.StartSector == Offset(sectorIndex, -1)));

            if (mediumFacility != null)
            {
                hex.Facilities.Remove(mediumFacility);

                var sectorsLeft = (mediumFacility.StartSector == sectorIndex) ?
                    Offset(mediumFacility.StartSector, 1) :
                    mediumFacility.StartSector;

                if (hex.Sectors[sectorsLeft].DefenceScore < MinBuildableSectorScore)
                    return;

                var newFacility = new Facility
                {
                    StartSector = sectorsLeft,
                    Size = FacilitySize.Small,
                    PlayerId = mediumFacility.PlayerId,
                    PrimaryColor = mediumFacility.PrimaryColor,
                    SecondaryColor = mediumFacility.SecondaryColor
                };

                hex.Facilities.Add(newFacility);
                ApplyFacilitiesToDefenceScore(hex, newFacility);

                return;
            }

            var largeFacility = hex.Facilities.FirstOrDefault(f =>
                f.Size == FacilitySize.Large &&
                (f.StartSector == sectorIndex ||
                 f.StartSector == Offset(sectorIndex, -1) ||
                 f.StartSector == Offset(sectorIndex, -2)));

            if (largeFacility != null)
            {
                hex.Facilities.Remove(largeFacility);
                if (largeFacility.StartSector == sectorIndex)
                {
                    Facility? newFacility = null;
                    if (hex.Sectors[Offset(largeFacility.StartSector, 1)].DefenceScore >= MinBuildableSectorScore &&
                        hex.Sectors[Offset(largeFacility.StartSector, 2)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = Offset(largeFacility.StartSector, 1),
                            Size = FacilitySize.Medium,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };
                        
                    } else if (hex.Sectors[Offset(largeFacility.StartSector, 1)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = Offset(largeFacility.StartSector, 1),
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };
                        
                    } else if (hex.Sectors[Offset(largeFacility.StartSector, 2)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = Offset(largeFacility.StartSector, 2),
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };
                        
                    }

                    if (newFacility != null)
                    {
                        hex.Facilities.Add(newFacility);
                        ApplyFacilitiesToDefenceScore(hex, newFacility);
                    }
                }
                else if (Offset(largeFacility.StartSector, 1) == sectorIndex)
                {
                    Facility? newFacility = null;
                    
                    if (hex.Sectors[largeFacility.StartSector].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = largeFacility.StartSector,
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };
                        hex.Facilities.Add(newFacility);
                        ApplyFacilitiesToDefenceScore(hex, newFacility);

                    }
                    if (hex.Sectors[Offset(largeFacility.StartSector, 2)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = Offset(largeFacility.StartSector, 2),
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };
                        hex.Facilities.Add(newFacility);
                        ApplyFacilitiesToDefenceScore(hex, newFacility);
                    }
                }
                else
                {
                    Facility? newFacility = null;
                    if (hex.Sectors[largeFacility.StartSector].DefenceScore >= MinBuildableSectorScore &&
                        hex.Sectors[Offset(largeFacility.StartSector, 1)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = largeFacility.StartSector,
                            Size = FacilitySize.Medium,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };

                    }
                    else if (hex.Sectors[largeFacility.StartSector].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = largeFacility.StartSector,
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };

                    }
                    else if (hex.Sectors[Offset(largeFacility.StartSector, 1)].DefenceScore >= MinBuildableSectorScore)
                    {
                        newFacility = new Facility
                        {
                            StartSector = Offset(largeFacility.StartSector, 1),
                            Size = FacilitySize.Small,
                            PlayerId = largeFacility.PlayerId,
                            PrimaryColor = largeFacility.PrimaryColor,
                            SecondaryColor = largeFacility.SecondaryColor
                        };

                    }

                    if (newFacility != null)
                    {
                        hex.Facilities.Add(newFacility);
                        ApplyFacilitiesToDefenceScore(hex, newFacility);
                    }
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
            if (absolute > 5)
            {
                return absolute - 6;
            }
            if (absolute < 0)
            {
                return absolute + 6;
            }
            return absolute;
        }
    }
}
