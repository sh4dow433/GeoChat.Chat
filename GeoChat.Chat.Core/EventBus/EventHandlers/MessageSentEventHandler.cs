using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.EventBus.Extensions;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Repos;
using Microsoft.Extensions.Configuration;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public class MessageSentEventHandler : IEventHandler<MessageSentEvent>
{
    private readonly IHubNotifier _hubNotifier;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IEventBus _bus;

    public MessageSentEventHandler(
        IHubNotifier hubNotifier,
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        IEventBus bus)
    {
        _hubNotifier = hubNotifier;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _bus = bus;
    }

    public async Task HandleAsync(MessageSentEvent @event)
    {
        var user = await _unitOfWork.UsersRepo.GetAsync(@event.DestinationUserId);
        
        if (user == null) throw new Exception("Message sender was null");

        if (user.SignalRConnectionId == null || user.RoutingKey == null)
        {
            // the user disconnected in the meantime
            return;
        }

        // check if the user is still connected to this server
        var localRoutingKey = _configuration["RabbitMq:SubscribeRoutings:MessageSentEvent:RoutingKey"];
        if (user.RoutingKey == localRoutingKey && 
            user.SignalRConnectionId == @event.DestinationConnectionId)
        {
            await _hubNotifier.SendMessage(@event.Message, @event.DestinationConnectionId);
            return;
        }

        // if the user is connected to another server reroute the message to that server
        var newEvent = new MessageSentEvent()
        {
            Message = @event.Message,
            DestinationConnectionId = user.SignalRConnectionId
        };
        _bus.PublishMessageSentEvent(_configuration, newEvent, user.RoutingKey);
    }
}