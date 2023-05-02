using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Repos;

public interface IUnitOfWork
{
    IUsersRepo UsersRepo { get; }
    IChatsRepo ChatsRepo { get; }
    IMessagesRepo MessagesRepo { get; }
    Task<int> SaveAsync();
}
