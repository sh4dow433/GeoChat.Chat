using GeoChat.Chat.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Repos;

public interface IUserChatsRepo : IGenericRepo<UserChat>
{
    Task<UserChat?> GetAsync(Expression<Func<UserChat, bool>> predicate);
    Task<IEnumerable<UserChat>> GetAllAsync(Expression<Func<UserChat, bool>> predicate);

}
