﻿using CastleCommander.WebApi.GameLogic.Enemies;
using MediatR;

namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class GetGame
    {
        public class Request : BaseGameRequest
        {
        }

        public class Handler(IGamesCache gamesCache, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCache, eventSender)
        {
            protected override Task<Game> Process(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Game);
            }

            protected override bool Validate()
            {
                return true;
            }
        }
    }
}
