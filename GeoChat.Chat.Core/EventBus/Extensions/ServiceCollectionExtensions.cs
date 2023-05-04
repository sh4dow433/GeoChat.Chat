using GeoChat.Chat.Core.EventBus.EventHandlers;
using GeoChat.Chat.Core.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<MessageSentEventHandler>();
        services.AddTransient<NewAccountCreatedEventHandler>();
        services.AddTransient<SyncResponseEventHandler>();
    }
}
