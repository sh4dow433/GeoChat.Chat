using GeoChat.Chat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Interfaces;

public interface IHubNotifier
{
    Task SendMessage(Message message, string connectionId);
    Task SendNewChatCreatedNotification(UserChat userChat, string connectionId);

}
