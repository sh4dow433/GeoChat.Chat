using GeoChat.Chat.Core.EventBus;
using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.EventBus.Extensions;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Services;

internal class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _bus;
    private readonly IConfiguration _config;
    private readonly string _locationChatName;
    private readonly int _locationChatId;

    public ChatService(IUnitOfWork unitOfWork, IEventBus bus, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _bus = bus;
        _config = config;

        var locationChatId = config["Location:Id"];
        if (locationChatId == null) throw new Exception("Location id is null");
        _locationChatId = int.Parse(locationChatId);

        _locationChatName = config["Location:Name"]!;
        if (_locationChatName == null) throw new Exception("Location name is null");
    }
    public async Task ConnectToChat(string userId, string connectionId)
    {
        // Change user connection id and routing key in DB
        var user = await _unitOfWork.UsersRepo.GetAsync(userId);
        if (user == null) throw new Exception("User couldnt be found");
        user.RoutingKey = _config["RabbitMq:SubscribeRoutings:MessageSentEvent:RoutingKey"];
        user.SignalRConnectionId = connectionId;
        _unitOfWork.UsersRepo.Update(user);

        // Add user to the location chat
        var userChat = new UserChat()
        {
            UserId = userId,
            User = user,
            ChatId = _locationChatId,   
            Chat = await GetLocationChatAsync(),
            Name = _locationChatName
        };

        _unitOfWork.UserChatsRepo.Create(userChat); 
        // save all the changes
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<UserChat>> GetUserChatsForUser(string userId)
    {
        var userchats = await _unitOfWork.UserChatsRepo.GetAllAsync(uc => uc.UserId == userId);
        return userchats;
    }

    public async Task CreateChatAsync(string userId, string friendUserId)
    {
        var user = await _unitOfWork.UsersRepo.GetAsync(userId);
        var friend = await _unitOfWork.UsersRepo.GetAsync(friendUserId);
        if (user == null || friend == null)
        {
            throw new Exception("User or friend doesnt exist");
        }

        var chat = new Models.Chat();
        var userChat = new UserChat()
        {
            User = user,
            Chat = chat,
            Name = friend.Name,
        };
        var friendChat = new UserChat()
        {
            User = friend,
            Chat = chat,
            Name = user.Name,
        };
     
        chat.UserChats.Add(userChat);
        chat.UserChats.Add(friendChat);
        _unitOfWork.ChatsRepo.Create(chat);
        await _unitOfWork.SaveAsync();

        // notify the user
        if (user.RoutingKey != null && user.SignalRConnectionId != null)
        {
            var newChatCreatedEvent = new NewChatCreatedEvent()
            {
                UserChat = userChat,
                ConnectionId = user.SignalRConnectionId
            };
            _bus.PublishChatCreatedEvent(_config, newChatCreatedEvent, user.RoutingKey);
        }

        // notify the friend
        if (friend.RoutingKey != null && friend.SignalRConnectionId != null)
        {
            var newChatCreatedEvent = new NewChatCreatedEvent()
            {
                UserChat = friendChat,
                ConnectionId = friend.SignalRConnectionId
            };
            _bus.PublishChatCreatedEvent(_config, newChatCreatedEvent, friend.RoutingKey);
        }
    }

    public async Task DisconnectFromChatAsync(string userId)
    {
        // Remove user connection id and routing key from DB
        var user = await _unitOfWork.UsersRepo.GetAsync(userId);
        if (user == null) throw new Exception("User couldnt be found");
        user.RoutingKey = null;
        user.SignalRConnectionId = null;
        _unitOfWork.UsersRepo.Update(user);

        // Remove user from location chat
        var userChat = await _unitOfWork.UserChatsRepo.GetAsync(uc => uc.UserId == userId && uc.ChatId == _locationChatId);
        if (userChat != null)
        {
            _unitOfWork.UserChatsRepo.Delete(userChat);
        }

        // Save all the changes
        await _unitOfWork.SaveAsync();
    }

    public async Task SendMessageAsync(Message message)
    {
        // save to db
        message.TimeSent = DateTime.Now;
        var msg = _unitOfWork.MessagesRepo.Create(message);
        await _unitOfWork.SaveAsync();


        // get all the users active in the chat
        var users = await _unitOfWork.ChatsRepo.GetChatUsers(message.ChatId);
        if (users == null) return;

        // foreach active user push an event to the queue
        foreach (var user in users)
        {
            if (user.RoutingKey != null && user.SignalRConnectionId != null)
            {
                var @event = new MessageSentEvent()
                {
                    Message = msg,
                    ConnectionId = user.SignalRConnectionId
                };
                _bus.PublishMessageSentEvent(_config, @event, user.RoutingKey);
            }
        }
    }
    private async Task<Models.Chat> GetLocationChatAsync() 
        => await _unitOfWork.ChatsRepo.GetAsync(c => c.LocationId == _locationChatId) ?? throw new Exception("LocationChat was null");
}
