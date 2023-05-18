using GeoChat.Chat.Core.Repos;
using GeoChat.Chat.Infra.DbAccess.Repos;

namespace GeoChat.Chat.Infra.DbAccess;

public class UnitOfWork : IUnitOfWork
{
    protected readonly AppDbContext _dbContext;

    public IUsersRepo UsersRepo { get; }

    public IChatsRepo ChatsRepo { get; }

    public IMessagesRepo MessagesRepo { get; }

    public IUserChatsRepo UserChatsRepo { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        UsersRepo = new UsersRepo(dbContext);
        ChatsRepo = new ChatsRepo(dbContext);
        MessagesRepo = new MessagesRepo(dbContext);
        UserChatsRepo = new UserChatsRepo(dbContext);
    }

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();
}
