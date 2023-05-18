using GeoChat.Chat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Interfaces;

public interface IChatService
{
    Task SendMessageAsync(Message message);
    Task ConnectToChat(string userId, string connectionId);
    Task DisconnectFromChatAsync(string userId);
    Task CreateChatAsync(string userId, string friendUserId);
    Task<IEnumerable<UserChat>> GetUserChatsForUser(string userId);
    void CreateLocationChatIfNotAvailable();
}
