using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; } = new();
        public List<UserChat> UserChats { get; set; } = new();
        public int? LocationId { get; set; }
    }
}