using CastleCommander.WebApi.GameLogic;
using CastleCommander.WebApi.GameLogic.Handlers;
using System.Runtime.CompilerServices;

namespace CastleCommander.WebApi
{
    public static class ApplicationConfigurationExtension
    {
        public static void ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(new[] {
                    typeof(StartNewGame).Assembly
                });
            });

            builder.Services.AddSingleton<IGamesCache, GamesCache>();
            builder.Services.AddSingleton<IGameFlow>(sp => GameFlowFactory.Create());
        }
    }
}
