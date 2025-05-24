using CastleCommander.WebApi.GameLogic.Enemies;

namespace CastleCommanderTests
{
    [TestFixture]
    internal class EnemyCardLoaderTest
    {
        [Test]
        public void ShouldLoadCards()
        {
            var file = Resource.EnemyDeck;
            var subject = new EnemyCardsLoader();

            var loaded = subject.Load(file);
            Assert.That(loaded, Is.Not.Null);
            Assert.That(loaded.Count, Is.EqualTo(20));
        }
    }
}
