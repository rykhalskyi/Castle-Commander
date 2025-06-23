using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public abstract class BaseGameHandler<T>(IGamesCache gamesCache) : IRequestHandler<T, Game> where T: IBaseGameRequest
    {
        protected Game Game { get; private set; }
        protected Player Player { get; private set; }

        public async Task<Game> Handle(T request, CancellationToken cancellationToken)
        {
            Game = GetGame(request);
            Player = Game.GetPlayer(request.PlayerId);
            if (!Validate())
            {
                Game.Log = "Player cannot perform this action now";
                Game.PlayerId = request.PlayerId;
                return Game;
            }

            var result = await Process(request, cancellationToken);
            result.PlayerId = request.PlayerId;
            return result;
        }

        protected abstract Task<Game> Process(T request, CancellationToken cancellationToken);

        private Game GetGame(T request)
        {
            var game = gamesCache.GetGame(request.GameId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }

            return game;
        }

        protected virtual bool Validate()
        {
            return Player.Id == Game.Players[Game.CurrentPlayer].Id;
        }
    }
}
