using MediatR;
using System.Numerics;

namespace CastleCommander.WebApi.GameLogic
{
    public class Market
    {
        public static bool Buy(ExchangeItem item, Player player)
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

        public static bool TryBuildFacility(Game game, FacilitySize size, int hexIndex)
        {
            var player = game.Players[game.CurrentPlayer];
            var price = hexIndex == 0 ? 2 : 1;
            switch (size) {
                case FacilitySize.Small:
                    if (player.Bronze < price) return false;
                    player.Bronze -= price;
                    break;
                case FacilitySize.Medium:
                    if (player.Silver < price) return false;
                    player.Silver -= price;
                    break;
                case FacilitySize.Large:
                    if (player.Gold < price) return false;
                    player.Gold -= price;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public static bool TryBuildTower(Game game, int hexIndex)
        {
            var player = game.Players[game.CurrentPlayer];
            var price = hexIndex == 0 ? 2 : 1;

            if (player.Gold < price) return false;
            player.Gold -= price;
            return true;
        }

        public static bool Exchange(Player player, 
            Player otherPlayer,
            int resource, 
            int otherPlayerResource)
        {

            if (player.Resources[resource].Number < 1 || 
                otherPlayer.Resources[otherPlayerResource].Number < 1) return false;

            player.Resources[resource].Number--;
            otherPlayer.Resources[resource].Number++;

            otherPlayer.Resources[otherPlayerResource].Number--;
            player.Resources[otherPlayerResource].Number++;

            return true;
        }

        public static bool TryRepairFacility(Game game, int hexIndex)
        {
            var player = game.Players[game.CurrentPlayer];
            var hex = game.Castle.Hexagons[hexIndex];
            var color = (int)hex.Color - 1;

            if (color < 0)return false; //market sector repair is not implemented yet
            if (player.Resources[color].Number < 1) return false;

            player.Resources[color].Number--;
            return true;

        }

        public static bool BuyResources(Player player, int resourceToSell, int resourceToBuy, int ratio)
        {
            if (player.Resources[resourceToSell].Number < 1)
            {
                return false; 
            }

            player.Resources[resourceToSell].Number--;
            player.Resources[resourceToBuy].Number += ratio;
            return true;
        }

        public static bool CanAttack(Player player, int hex) {
            if (hex == 0)
            {
                if (player.Gold <1) return false;
                player.Gold--;
                return true;
            }
            else
            {
                if (player.Bronze < 1) return false;
                player.Bronze--;
                return true;
            }
            
        }

        public static bool BuyCoins(int[] resources, ExchangeItem item, Player player) {
            var grouped = resources.GroupBy(i => i);
            foreach (var group in grouped)
            {
                if (player.Resources[group.Key].Number < group.Count())
                    return false;
            }

            switch (item)
            {
                case ExchangeItem.Bronze:
                    player.Bronze++;
                    break;
                case ExchangeItem.Silber:
                    if (player.Bronze < 1) return false;
                    player.Bronze--;
                    player.Silver++;
                    break;
                case ExchangeItem.Gold:
                    if (player.Bronze < 1 || player.Silver < 1) return false;
                    player.Bronze--;
                    player.Silver--;
                    player.Gold++;
                    return true;
            }

            foreach (var group in grouped)
            {
                player.Resources[group.Key].Number -= group.Count();
            }

            return true;
        }

    }
}
