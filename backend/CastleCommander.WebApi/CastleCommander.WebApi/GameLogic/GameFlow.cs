namespace CastleCommander.WebApi.GameLogic
{
    public class GameFlow : IGameFlow
    {
        public IList<IGameTurn> Turns { get; set; }

        public Game StartGame(Game userInput)
        {
            var game = GameFlowFactory.CreateGame();
            game.TurnMessage = "Game started";

            return game;
        }

        public Game NextTurn(Game userInput, Game game)
        {
            Turns[game.CurrentTurn].UpdateGame(userInput, game);

            if (!Turns[game.CurrentTurn].CanTurn(game)) return game;

            game.CurrentTurn++;
            if (game.CurrentTurn >= Turns.Count) game.CurrentTurn = 0;

            Turns[game.CurrentTurn].MakeTurn(userInput, game);
            return game;

        }
    }
}
