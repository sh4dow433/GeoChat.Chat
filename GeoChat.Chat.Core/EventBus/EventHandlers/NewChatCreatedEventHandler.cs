using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.EventBus.Extensions;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Repos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public class NewChatCreatedEventHandler : IEventHandler<NewChatCreatedEvent>
{
    private readonly IHubNotifier _hubNotifier;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IEventBus _bus;

    public NewChatCreatedEventHandler(
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
    public async Task HandleAsync(NewChatCreatedEvent @event)
    {
        var userId = @event.UserChat.UserId;
        var user = await _unitOfWork.UsersRepo.GetAsync(userId);
        if (user == null) throw new Exception("User is null");

        if (user.SignalRConnectionId == null || user.RoutingKey == null)
        {
            // the user disconnected in the meantime
            return;
        }

        // check if the user is still connected to this server
        var localRoutingKey = _configuration["RabbitMq:SubscribeRoutings:MessageSentEvent:RoutingKey"];
        if (user.RoutingKey == localRoutingKey &&
            user.SignalRConnectionId == @event.ConnectionId)
        {
            await _hubNotifier.SendNewChatCreatedNotification(@event.UserChat, @event.ConnectionId);
            return;
        }

        // if the user is connected to another server reroute the event to that server
        var newEvent = new NewChatCreatedEvent()
        {
            UserChat = @event.UserChat,
            ConnectionId = user.SignalRConnectionId
        };
        _bus.PublishChatCreatedEvent(_configuration, newEvent, user.RoutingKey);
    }
}
