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
        public string Content { get; set; }
        public User Sender { get; set; }
        public DateTime TimeSent { get; set; }

    }
}
