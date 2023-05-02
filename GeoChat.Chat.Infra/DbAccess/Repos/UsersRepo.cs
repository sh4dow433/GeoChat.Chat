using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using Microsoft.EntityFrameworkCore;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class UsersRepo : GenericRepo<User>, IUsersRepo
{
    public UsersRepo(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddOrUpdateBatchAsync(IEnumerable<User> users)
    {
        foreach (var user in users) 
        {
            var doesUserExist = await EntitySet.AnyAsync(u => u.Id == user.Id);
            if (doesUserExist)
            {
                EntitySet.Update(user);
            }
            else
            {
                EntitySet.Add(user);
            }
        }
    }
}
