
namespace CastleCommander.WebApi.GameLogic
{
    public class PlayerCreator
    {
        public static Game AddPlayer(Game game)
        {
            if (game.Players.Count >= 4) {
                game.Log = "there's already 4 players in the game, you can't add more players.";
                return game;
            }
            
            var newPlayer = Create(game.Players.Count);

            game.Players.Add(newPlayer);
            game.PlayerId = newPlayer.Id;

            return game;
        }

        private static List<Player> _players = new List<Player>()
                {
                    new Player()
                    {
                        Name = "Jim Bon Jivi",
                        IsActive = true,
                        Resources = GetPlayerResources(new[]{1,2,3}),
                        PrimaryColor = "#490101",
                        SecondaryColor = "#970000",
                        Bronze = 1,
                        Silver =1,
                        Gold = 1

                    },
                    new Player()
                    {
                        Name = "Eazy Earsborn",
                        Resources = GetPlayerResources(new[]{1,3,5}),
                        PrimaryColor = "#014905",
                        SecondaryColor = "#009721",
                        Bronze = 1,
                        Silver =1,
                        Gold = 1
                    },
                    new Player()
                    {
                        Name = "Linn Tildelman",
                        Resources = GetPlayerResources(new[]{2,4,6}),
                        PrimaryColor = "#011549",
                        SecondaryColor = "#005D97",
                        Bronze = 1,
                        Silver =1,
                        Gold = 1
                    },
                    new Player()
                    {
                        Name = "Stiggy Zardust",
                        Resources = GetPlayerResources(new[]{4,5,6}),
                        PrimaryColor = "#493601",
                        SecondaryColor = "#975D00",
                        Bronze = 1,
                        Silver =1,
                        Gold = 1
                    },
                };

        private static Player Create(int index)
        {
            var player = _players[index];
            return new Player
            {
                Id = Guid.NewGuid(),
                Name = player.Name,
                IsActive = player.IsActive,
                PrimaryColor = player.PrimaryColor,
                SecondaryColor = player.SecondaryColor,
                Bronze = player.Bronze,
                Silver = player.Silver,
                Gold = player.Gold,
                Resources = GetPlayerResources(new[] { 1, 2, 3 }) // This should be replaced with actual logic to set resources
            };
        }


        private static PlayerResource[] GetPlayerResources(int[] baseResources)
        {
            var result = new PlayerResource[]
            {
                new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Orange],
                },
                     new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Yellow],
                },
                          new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Green],
                },
                               new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Blue],
                },
                                    new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Navy],
                },
                                         new PlayerResource
                {
                    Number = 2,
                    Color = HexagonColors.Values[(int)HexagonColor.Purple],
                }
            };

            for (int i = 0; i < baseResources.Length; i++)
            {
                var resource = baseResources[i] - 1;
                result[resource].IsBase = true;
            }

            return result;
        }
    }
}
