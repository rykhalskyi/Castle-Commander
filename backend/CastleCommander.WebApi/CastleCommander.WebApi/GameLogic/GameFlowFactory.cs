
using CastleCommander.WebApi.GameLogic.Turns;

namespace CastleCommander.WebApi.GameLogic
{
    public static class GameFlowFactory
    {
        public static IGameFlow Create()
        {
            return new GameFlow()
            {
                Turns = new List<IGameTurn>()
                {
                   new ResourceCollectionTurn(),
                   new EnemyCardPickTurn(),
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
                        new Hexagon() { Color = HexagonColor.Red },
                        new Hexagon() { Color = HexagonColor.Orange },
                        new Hexagon() { Color = HexagonColor.Yellow },
                        new Hexagon() { Color = HexagonColor.Green },
                        new Hexagon() { Color = HexagonColor.Blue },
                        new Hexagon() { Color = HexagonColor.Navy },
                        new Hexagon() { Color = HexagonColor.Purple },
                                            }
                },
                Players = new List<Player>()
                {
                    new Player()
                    {
                        Name = "Jim Bon Jivi",
                        IsActive = true,
                        Resources = GetPlayerResources(),
                    },
                    new Player()
                    {
                        Name = "Eazy Earsborn",
                        Resources = GetPlayerResources()

                    },
                    new Player()
                    {
                        Name = "Linn Tildelman",
                        Resources = GetPlayerResources()
                    },
                    new Player()
                    {
                        Name = "Stiggy Zardust",
                        Resources = GetPlayerResources()

                    },
                }
            };
        }

        private static PlayerResource[] GetPlayerResources()
        {
            return new PlayerResource[]
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
        }
    }
}
