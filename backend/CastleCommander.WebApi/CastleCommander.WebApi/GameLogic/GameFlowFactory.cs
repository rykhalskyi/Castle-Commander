﻿
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
                   new FightTurn(enemyCardsCache),
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
