using GeoChat.Chat.Api.Hubs;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace GeoChat.Chat.Api.Services;

public class HubNotifier : IHubNotifier
{
    private readonly IHubContext<ChatHub> _hubContext;

    public HubNotifier(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(Message message, string connectionId)
    {
        await _hubContext
            .Clients
            .Client(connectionId)
            .SendAsync("MessageReceived", message);
    }
    
    public async Task SendNewChatCreatedNotification(UserChat userChat, string connectionId)
    {
        await _hubContext
            .Clients
            .Client(connectionId)
            .SendAsync("ChatCreated", userChat);
    }
}
