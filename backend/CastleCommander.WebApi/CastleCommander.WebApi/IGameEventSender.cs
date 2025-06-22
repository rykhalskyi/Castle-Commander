public interface IGameEventSender
{
    /// <summary>
    /// Sends a game update event to connected clients.
    /// </summary>
    /// <param name="gameId">The unique identifier of the game.</param>
    /// <param name="playerId">The unique identifier of the player.</param>
    Task NextUpdate(Guid gameId, Guid playerId);
}
