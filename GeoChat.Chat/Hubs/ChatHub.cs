using GeoChat.Chat.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GeoChat.Chat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                throw new Exception("User or user id is null");
            }
            await _chatService.ConnectToChat(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("Id")?.Value;
            if (userId == null)
            {
                throw new Exception("User or userid is null");
            }
            await _chatService.DisconnectFromChatAsync(userId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
