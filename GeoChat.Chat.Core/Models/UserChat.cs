namespace GeoChat.Chat.Core.Models
{
    public class UserChat
    {
        public string Name { get; set; } = null!;

        public int ChatId { get; set; }

        public string UserId { get; set; } = null!;

        public Chat Chat { get; set; } = null!;

        public User User { get; set; } = null!; 
    }
}