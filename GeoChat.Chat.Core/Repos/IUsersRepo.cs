using GeoChat.Chat.Core.Models;

namespace GeoChat.Chat.Core.Repos;

public interface IUsersRepo : IGenericRepo<User>
{
    Task AddOrUpdateBatchAsync(IEnumerable<User> users);
    Task<DateTime> GetLastUpdate();
    Task<IEnumerable<User>> GetUsersByName(string name);
}
