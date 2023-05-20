using GeoChat.Chat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.Events;

public class NewChatCreatedEvent : BaseEvent
{
    public UserChat UserChat { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;
}
