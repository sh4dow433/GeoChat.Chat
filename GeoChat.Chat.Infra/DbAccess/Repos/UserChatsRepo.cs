using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

internal class UserChatsRepo : GenericRepo<UserChat>, IUserChatsRepo
{
    public UserChatsRepo(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<UserChat?> GetAsync(Expression<Func<UserChat, bool>> filter)
    {
        return await EntitySet.Where(filter).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<UserChat>> GetAllAsync(Expression<Func<UserChat, bool>> filter)
    {
        var userChats = await EntitySet
            .Include(uc => uc.User)
            .Include(uc => uc.Chat)
                .ThenInclude(c => c.UserChats)
                    .ThenInclude(uc2 => uc2.User)
            .Include(uc => uc.Chat)
                .ThenInclude(c => c.Messages
                        .OrderByDescending(m => m.TimeSent)
                        .Take(50))
                    .ThenInclude(m => m.User)
            .OrderBy(uc => uc.Chat.LocationId == null)
            .Where(filter)
            .ToListAsync();

        return userChats;   
    }
}
