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

        [Test]
        public void FacilityHelper_Deletes_Small_Facility()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Small,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;

            FacilityHelper.CheckAndDestroy(hexagon, 0);

            Assert.That(hexagon.Facilities.Count, Is.EqualTo(0));
        }

        [Test]
        public void FacilityHelper_Deletes_Medium_Facility_and_creates_a_small()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Medium,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;

            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i=>i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 1), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Medium_Facility_and_creates_a_small_2()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Medium,
                StartSector = 5
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;

            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 5), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Medium_Facility_and_do_not_creates_a_small()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Medium,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            hexagon.Sectors[1].DefenceScore = -1; // This sector is destroyed, so no small facility will be created

            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 1), Is.EqualTo(0));
        }

        [Test]
        public void FacilityHelper_Deletes_Medium_Facility_and_do_not_creates_a_small_2()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Medium,
                StartSector = 5
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            hexagon.Sectors[5].DefenceScore = -1; // This sector is destroyed, so no small facility will be created

            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 1), Is.EqualTo(0));
        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_medium()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium && i.StartSector == 1), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_medium_2()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 4
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium && i.StartSector == 4), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_two_small()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 5
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 5), Is.EqualTo(1));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 1), Is.EqualTo(1));

        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_small_1()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            hexagon.Sectors[1].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 2), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_small_2()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            hexagon.Sectors[2].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small && i.StartSector == 1), Is.EqualTo(1));
        }

        [Test]
        public void FacilityHelper_Deletes_Large_Facility_and_creates_no_small()
        {
            var hexagon = new Hexagon
            {
                Facilities = new List<Facility>()
            };
            var facility = new Facility
            {
                Size = FacilitySize.Large,
                StartSector = 0
            };
            hexagon.Facilities.Add(facility);
            hexagon.Sectors[0].DefenceScore = -1;
            hexagon.Sectors[1].DefenceScore = -1;
            hexagon.Sectors[2].DefenceScore = -1;
            FacilityHelper.CheckAndDestroy(hexagon, 0);
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Large), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Medium), Is.EqualTo(0));
            Assert.That(hexagon.Facilities.Count(i => i.Size == FacilitySize.Small), Is.EqualTo(0));
        }
    }
}