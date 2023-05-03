using GeoChat.Chat.Core.EventBus.Events;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public class MessageSentEventHandler : IEventHandler<MessageSentEvent>
{
    public Task HandleAsync(MessageSentEvent @event)
    {
        throw new NotImplementedException();
    }
}