using GeoChat.Chat.Core.Repos;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class ChatsRepo : GenericRepo<Core.Models.Chat>, IChatsRepo
{
    public ChatsRepo(AppDbContext dbContext) : base(dbContext)
    {
    }
}
