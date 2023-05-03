using GeoChat.Chat.Core.EventBus.EventHandlers;
using GeoChat.Chat.Core.EventBus.Events;

namespace GeoChat.Chat.Core.EventBus;

public class MockEventBus : IEventBus
{
    public void Dispose()
    {
        Console.WriteLine("Disposing eventbus");
    }

    public void PublishEvent<TEvent>(TEvent @event, string exchange, string exchangeType, string routingKey = "") where TEvent : BaseEvent
    {
        Console.WriteLine($"Publishing {typeof(TEvent).Name} to: {exchange}:{exchangeType}:{routingKey}");
    }

    public void Subscribe<TEvent, TEventHandler>()
        where TEvent : BaseEvent
        where TEventHandler : IEventHandler<TEvent>
    {
        Console.WriteLine($"Added handler {typeof(TEventHandler).Name} for the event {typeof(TEvent).Name}");
    }

    public void Unsubscribe<TEvent, TEventHandler>()
        where TEvent : BaseEvent
        where TEventHandler : IEventHandler<TEvent>
    {
        Console.WriteLine($"Removed handler {typeof(TEventHandler).Name} of the event {typeof(TEvent).Name}");
    }
}
