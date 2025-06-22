using System.Collections.Concurrent;

public class SseGameEventSender : IGameEventSender
{
    // Store a list of connected clients (for demo, use a ConcurrentDictionary)
    private readonly ConcurrentDictionary<Guid, List<HttpResponse>> _clients = new();

    // Register a client connection (call this from your SSE endpoint)
    public void RegisterClient(Guid gameId, HttpResponse response)
    {
        _clients.AddOrUpdate(
            gameId,
            _ => new List<HttpResponse> { response },
            (_, list) => { list.Add(response); return list; }
        );
    }

    public async Task NextUpdate(Guid gameId, Guid playerId)
    {
        if (_clients.TryGetValue(gameId, out var responses))
        {
            var data = $"data: {{ \"gameId\": \"{gameId}\", \"playerId\": \"{playerId}\" }}\n\n";
            foreach (var response in responses.ToList())
            {
                
                try
                {
                    await response.WriteAsync(data);
                    await response.Body.FlushAsync();
                }
                catch
                {
                    // Remove disconnected clients
                    responses.Remove(response);
                }
            }
        }
    }
    
}
