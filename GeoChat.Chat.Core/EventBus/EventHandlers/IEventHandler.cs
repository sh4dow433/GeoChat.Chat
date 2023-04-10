using GeoChat.Chat.Core.EventBus.Events;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public interface IEventHandler<TEvent> where TEvent : BaseEvent
{
    Task HandleAsync(TEvent @event);
}
