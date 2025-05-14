namespace CastleCommander.WebApi.GameLogic
{
    public class Market
    {
        public static bool Exchange(ExchangeItem item, Player player)
        {
            switch (item)
            {
                case ExchangeItem.Bronze:
                    var baseResources = player.Resources.Where(i => i.IsBase).ToList();
                    if (baseResources.Any(i => i.Number < 1)) return false;
                    baseResources.ForEach(i => i.Number--);
                    player.Bronze++;
                    return true;
                case ExchangeItem.Silber:
                    var nonBaseResources = player.Resources.Where(i => !i.IsBase).ToList();
                    if (nonBaseResources.Any(i => i.Number < 1) || player.Bronze < 1) return false;
                    nonBaseResources.ForEach(i => i.Number--);
                    player.Bronze--;
                    player.Silver++;
                    return true;
                case ExchangeItem.Gold:
                    var resources = player.Resources.ToList();
                    if (resources.Any(i=>i.Number < 1) || player.Silver < 1) return false;
                    resources.ForEach(i => i.Number--);
                    player.Silver--;
                    player.Gold++;
                    return true;
                default:
                    return false;
            }
        }   
    }
}
