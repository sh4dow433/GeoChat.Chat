using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public User User { get; set; } = null!;
        public Chat Chat { get; set; } = null!;
        public DateTime TimeSent { get; set; }
    }
}
