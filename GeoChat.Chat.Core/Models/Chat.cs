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
        public string Name { get; set; }
        public Message[] Messages { get; set; }

        public int LocationId { get; set; }

    }
}
