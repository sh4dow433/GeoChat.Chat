using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class MessagesRepo : GenericRepo<Message>, IMessagesRepo
{
    public MessagesRepo(AppDbContext dbContext) : base(dbContext)
    {
    }
}
