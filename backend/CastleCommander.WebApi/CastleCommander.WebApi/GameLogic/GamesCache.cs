namespace CastleCommander.WebApi.GameLogic
{
    public class GamesCache : IGamesCache
    {
        private readonly Dictionary<Guid, Game> _games = new();

        public void AddGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            if (_games.ContainsKey(game.Id)) throw new InvalidOperationException("Game already exists in cache.");
            _games[game.Id] = game;
        }

        public Game GetGame(Guid gameId)
        {
            if (_games.TryGetValue(gameId, out var game))
            {
                return game;
            }
            throw new KeyNotFoundException($"Game with ID {gameId} not found in cache.");
        }
    }
}
