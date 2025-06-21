namespace CastleCommander.WebApi.GameLogic
{
    public static class GameExtentions
    {
        public static Player GetPlayer(this Game game, Guid playerId)
        {
            return game.Players.FirstOrDefault(p => p.Id == playerId) ?? throw new Exception("Player not found");
        }
    }
}
