using CastleCommander.WebApi.GameLogic;
using CastleCommander.WebApi.GameLogic.Enemies;
using CastleCommander.WebApi.GameLogic.Handlers;
using System.Runtime.CompilerServices;

namespace CastleCommander.WebApi
{
    public static class ApplicationConfigurationExtension
    {
        public static void ConfigureDependencies(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddSingleton<IConfiguration>(configuration);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(new[] {
                    typeof(StartNewGame).Assembly
                });
            });

            builder.Services.AddSingleton<IEnemyCardsCache>(sp =>
            {
                var cardFilePath = configuration.GetValue<string>("EnemyCardsFilePath");
                return new EnemyCardsCache(cardFilePath);
            });

            builder.Services.AddSingleton<IGamesCache, GamesCache>();
            builder.Services.AddSingleton<IGameFlow>(sp => 
            {
                var enemyCardsCache = sp.GetRequiredService<IEnemyCardsCache>();
                return GameFlowFactory.Create(enemyCardsCache);
            });
            builder.Services.AddSingleton<IGameEventSender, SseGameEventSender>();

        }
    }
}
