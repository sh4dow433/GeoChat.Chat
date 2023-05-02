using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class UsersRepo : GenericRepo<User>, IUsersRepo
{
    public UsersRepo(AppDbContext dbContext) : base(dbContext)
    {
    }
}
