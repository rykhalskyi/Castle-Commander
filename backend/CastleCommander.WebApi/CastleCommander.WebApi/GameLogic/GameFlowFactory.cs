
using CastleCommander.WebApi.GameLogic.Enemies;
using CastleCommander.WebApi.GameLogic.Turns;

namespace CastleCommander.WebApi.GameLogic
{
    public static class GameFlowFactory
    {
        public static IGameFlow Create(IEnemyCardsCache enemyCardsCache)
        {
            return new GameFlow()
            {
                Turns = new List<IGameTurn>()
                {
                   new ResourceCollectionTurn(),
                   new EnemyCardPickTurn(enemyCardsCache),
                   new TradeAndBuildTurn(),
                   new FightTurn(),
                   new AfterFightTurn(),
                }
            };
        }

        public static Game CreateGame()
        {
            return new Game()
            {
                Castle = new Castle()
                {
                    Hexagons = new List<Hexagon>()
                    {
                        new Hexagon() { ColorValue = HexagonColors.Values[(int) HexagonColor.Red], Color = HexagonColor.Red},
                        new Hexagon() { ColorValue = HexagonColors.Values[(int)HexagonColor.Orange], Color = HexagonColor.Orange },
                        new Hexagon() { ColorValue = HexagonColors.Values[(int)HexagonColor.Yellow], Color = HexagonColor.Yellow },
                        new Hexagon() { ColorValue = HexagonColors.Values[(int)HexagonColor.Green] , Color = HexagonColor.Green},
                        new Hexagon() { ColorValue = HexagonColors.Values[(int) HexagonColor.Blue] , Color = HexagonColor.Blue},
                        new Hexagon() { ColorValue = HexagonColors.Values[(int) HexagonColor.Navy] , Color = HexagonColor.Navy},
                        new Hexagon() { ColorValue = HexagonColors.Values[(int) HexagonColor.Purple], Color = HexagonColor.Purple},
                                            }
                },
                Players = new List<Player>()
                {
                    new Player()
                    {
                        Name = "Jim Bon Jivi",
                        IsActive = true,
                        Resources = GetPlayerResources(new[]{1,2,3}),
                        PrimaryColor = "#490101",
                        SecondaryColor = "#970000",

                    },
                    new Player()
                    {
                        Name = "Eazy Earsborn",
                        Resources = GetPlayerResources(new[]{1,3,5}),
                        PrimaryColor = "#014905",
                        SecondaryColor = "#009721",
                    },
                    new Player()
                    {
                        Name = "Linn Tildelman",
                        Resources = GetPlayerResources(new[]{2,4,6}),
                        PrimaryColor = "#011549",
                        SecondaryColor = "#005D97",
                    },
                    new Player()
                    {
                        Name = "Stiggy Zardust",
                        Resources = GetPlayerResources(new[]{4,5,6}),
                        PrimaryColor = "#493601",
                        SecondaryColor = "#975D00",

                    },
                }
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
