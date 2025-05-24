namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public abstract class BaseEnemyCard
    {
        public string Description { get; set; } = string.Empty;
    }

    public class EnemyCard : BaseEnemyCard
    {
        public int HexNumber { get; set; }
        public int SectorsNumber { get; set; }
        public int MaxForce { get; set; }
    }

    public class BonusCard : BaseEnemyCard {
        public int HexNumber { get; set; }
        public int SectorsNumber { get; set; }
        public int RestoreValue { get; set; }
    }

    public class EventCard : BaseEnemyCard {
        public int Duration { get; set; }
        public int Defence { get; set; }
        public int EnemyAttack { get; set; }
    }
}
