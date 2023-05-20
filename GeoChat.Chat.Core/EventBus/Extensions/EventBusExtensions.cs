using GeoChat.Chat.Core.EventBus.Events;
using Microsoft.Extensions.Configuration;

namespace GeoChat.Chat.Core.EventBus.Extensions;

public static class EventBusExtensions
{
    public static void PublishMessageSentEvent(this IEventBus eventBus, IConfiguration configuration, MessageSentEvent @event, string routing)
    {
        var baseCfg = $"RabbitMq:PublishRoutings:{nameof(MessageSentEvent)}";

        var exchange = configuration[$"{baseCfg}:Exchange"];
        var exchangeType = configuration[$"{baseCfg}:ExchangeType"];
        if (exchange == null || exchangeType == null)
        {
            throw new Exception("The exchange or exchange type wasn't configured in the appsettings.json file.");
        }
        eventBus.PublishEvent(@event, exchange, exchangeType, routing);
    }

    public static void PublishChatCreatedEvent(this IEventBus eventBus, IConfiguration configuration, NewChatCreatedEvent@event, string routing)
    {
        var baseCfg = $"RabbitMq:PublishRoutings:{nameof(NewChatCreatedEvent)}";
        var exchange = configuration[$"{baseCfg}:Exchange"];
        var exchangeType = configuration[$"{baseCfg}:ExchangeType"];
        if (exchange == null || exchangeType == null)
        {
            throw new Exception("The exchange or exchange type wasn't configured in the appsettings.json file.");
        }
        eventBus.PublishEvent(@event, exchange, exchangeType, routing);
    }
}
