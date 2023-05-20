using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class ChatsRepo : GenericRepo<Core.Models.Chat>, IChatsRepo
{
    public ChatsRepo(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Core.Models.Chat?> GetAsync(Expression<Func<Core.Models.Chat, bool>> filter)
    {
        return await EntitySet.Include(c => c.UserChats).Where(filter).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<User>?> GetChatUsers(int chatId)
    {
        return await EntitySet
            .Include(c => c.UserChats)
                .ThenInclude(uc => uc.User)
            .Where(c => c.Id == chatId)
            .Select(c => c.UserChats.Select(uc => uc.User))
            .FirstOrDefaultAsync();
    }


}
