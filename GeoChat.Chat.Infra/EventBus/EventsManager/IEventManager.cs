using GeoChat.Chat.Core.EventBus.EventHandlers;
using GeoChat.Chat.Core.EventBus.Events;

namespace GeoChat.Chat.Infra.EventBus.EventsManager;

public interface IEventManager
{
    void Subscribe<TEvent, TEventHandler>()
        where TEvent : BaseEvent
        where TEventHandler : IEventHandler<TEvent>;

    void Unsubscribe<TEvent, TEventHandler>()
        where TEvent : BaseEvent
        where TEventHandler : IEventHandler<TEvent>;


    IEnumerable<Type> GetHandlers(string eventName);
    int GetHandlersCount<TEvent>() where TEvent : BaseEvent;


    Type GetEventTypeFromRoutingKey(string exchange, string routingKey);
    EventDetails GetEventDetails<TEvent>() where TEvent : BaseEvent;
}
