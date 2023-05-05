using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.Events;

public class SyncCallEvent : BaseEvent
{
    public DateTime LastSyncCall { get; set; }
}
