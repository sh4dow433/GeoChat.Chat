using AutoMapper;
using GeoChat.Chat.Api.Dtos;
using GeoChat.Chat.Api.Hubs;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace GeoChat.Chat.Api.Services;

public class HubNotifier : IHubNotifier
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IMapper _mapper;

    public HubNotifier(IHubContext<ChatHub> hubContext, IMapper mapper)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public async Task SendMessage(Message message, string connectionId)
    {
        var mappedMessage = _mapper.Map<MessageReadDto>(message);
        await _hubContext
            .Clients
            .Client(connectionId)
            .SendAsync("MessageReceived", mappedMessage);
    }
    
    public async Task SendNewChatCreatedNotification(UserChat userChat, string connectionId)
    {
        var mappedChat = _mapper.Map<ChatReadDto>(userChat);
        await _hubContext
            .Clients
            .Client(connectionId)
            .SendAsync("ChatCreated", mappedChat);
    }
}
