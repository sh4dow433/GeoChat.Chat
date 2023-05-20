using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime LastUpdated { get; set; }
        public string? RoutingKey { get; set; }
        public string? SignalRConnectionId { get; set; }

        public List<UserChat> UserChats { get; set; } = new();
    }
}
