namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public class CurrentCards
    {
        public CardDto? ImpactCard { get; set; }

        public List<CardDto> EventCards { get; set; } = new List<CardDto>();

        public class CardDto
        {
            public int Index { get; set; }
            public string Description { get; set; } = string.Empty;
            public int Value { get; set; }
        }
    }

    
}
