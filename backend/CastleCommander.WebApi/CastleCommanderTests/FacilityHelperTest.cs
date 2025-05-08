using CastleCommander.WebApi.GameLogic;

namespace CastleCommanderTests
{
    public class FacilityHelperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FacilityHelper_Checks_Fit_Small_Facility()
        { 
            var hexagon = new Hexagon
            {
            };

            var facility = new Facility
            {
                Size = FacilitySize.Small,
                StartSector = 1
            };

            var fits = FacilityHelper.DoesFit(hexagon,facility);
            Assert.That(fits, Is.True);

            hexagon.Facilities.Add(facility);

            fits = FacilityHelper.DoesFit(hexagon, facility);
            Assert.That(fits, Is.False);
        }
    }
}