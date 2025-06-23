
namespace CastleCommander.WebApi.GameLogic.Handlers
{
    public class TowerAttack
    {
        public class Request : BaseGameRequest
        {
            public int Hexagon { get; set; }
        }

        public class Handler(IGamesCache gamesCahce, IGameEventSender eventSender) : BaseGameHandler<Request>(gamesCahce, eventSender)
        {
            protected override async Task<Game> Process(Request request, CancellationToken cancellationToken)
            {

                var tower = Game.Castle.Hexagons[request.Hexagon].Tower;
                if (tower == null || tower.PlayerId != Player.Id)
                {
                    Game.Log += "You have no tower in this hex \n";
                    return await Task.FromResult(Game);
                }

                if (!Market.CanAttack(Player, request.Hexagon))
                {
                    Game.Log += "You have no enough bronye to attack \n";
                    return await Task.FromResult(Game);
                }

                if (request.Hexagon == 0) 
                {
                    //Let's eliminate all threats if market attacks
                    for (int i=1; i<7; i++)
                        ClearImpactValue(i, Game);
                }
                else
                {
                    ClearImpactValue(request.Hexagon, Game);
                }


                return await Task.FromResult(Game);
            }

            private static void ClearImpactValue(int hex, Game game)
            {
                foreach (var sector in game.Castle.Hexagons[hex].Sectors)
                {
                    sector.ImpactValue = 0;
                }
                game.Log += $"All threats in hexagon {hex} are eliminated \n";
            }
        }
    }
}
