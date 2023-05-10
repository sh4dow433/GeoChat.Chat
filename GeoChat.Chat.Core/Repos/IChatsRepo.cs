using GeoChat.Chat.Core.Models;
using System.Linq.Expressions;

namespace GeoChat.Chat.Core.Repos;

public interface IChatsRepo : IGenericRepo<Models.Chat>
{
    Task<Models.Chat?> GetAsync(Expression<Func<Models.Chat, bool>> predicate);
    Task<IEnumerable<User>?> GetChatUsers(int chatId);
}