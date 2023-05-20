using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.Events;

public class NewAccountCreatedEvent : BaseEvent
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
