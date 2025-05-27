using System.Text.Json;
using System.Text.Json.Nodes;

namespace CastleCommander.WebApi.GameLogic.Enemies
{
    public class EnemyCardsLoader
    {
        public List<BaseEnemyCard> Load(string? file)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file), "Input file cannot be null or empty.");
            }

            var cardArray = JsonNode.Parse(file) as JsonArray;
            if (cardArray == null)
            {
                throw new JsonException("Failed to parse JSON array.");
            }

            var result = new List<BaseEnemyCard>();

            foreach (var card in cardArray)
            {
                var cardType = card?["type"]?.GetValue<string>();
                var cardDescription = card?["description"]?.GetValue<string>() ?? string.Empty;
                if (cardType == null)
                {
                    continue;
                }

                switch (cardType)
                {
                    case "impact":
                        result.Add(new EnemyCard
                        {
                            Description = cardDescription,
                            HexNumber = int.TryParse(card?["hexNumber"]?.GetValue<string>(), out var hexNumber) ? hexNumber : 0,
                            SectorsNumber = int.TryParse(card?["sectorNumber"]?.GetValue<string>(), out var sectorsNumber) ? sectorsNumber : 0,
                            ImpactValue = int.TryParse(card?["impactValue"]?.GetValue<string>(), out var maxForce) ? maxForce : 0
                        });
                        break;
                   
                    case "event":
                        result.Add(new EventCard
                        {
                            Description = cardDescription,
                            Duration = int.TryParse(card?["duration"]?.GetValue<string>(), out var duration) ? duration : 0,
                            Defence = int.TryParse(card?["defence"]?.GetValue<string>(), out var defence) ? defence : 1,
                            EnemyAttack = int.TryParse(card?["enemyAttack"]?.GetValue<string>(), out var enemyAttack) ? enemyAttack : 1
                        });
                        break;
                }
            }

            return result;
        }

        public List<BaseEnemyCard> Load(byte[] file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file), "Input file cannot be null or empty.");
            }

            var fileString = System.Text.Encoding.UTF8.GetString(file);
            return Load(fileString);
        }
    }
}
