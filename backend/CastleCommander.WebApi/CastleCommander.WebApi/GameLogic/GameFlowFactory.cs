
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
            return new Game();
        }
    }
}
