using GeoChat.Chat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.Events;

public class MessageSentEvent : BaseEvent
{
    public Message Message { get; set; } = null!;
    public string DestinationConnectionId { get; set; } = null!;
    public string DestinationUserId { get; set; } = null!;
}
