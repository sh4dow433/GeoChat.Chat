using GeoChat.Chat.Infra.EventBus.ConnectionManager;
using GeoChat.Chat.Infra.EventBus.EventsManager;
using Microsoft.Extensions.DependencyInjection;

namespace GeoChat.Chat.Infra.EventBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventManager, EventManager>();
        services.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();
        services.AddSingleton<IEventBus, EventBus>();
    }
}
