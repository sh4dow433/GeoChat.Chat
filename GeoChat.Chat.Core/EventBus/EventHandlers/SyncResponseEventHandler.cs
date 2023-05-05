using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public class SyncResponseEventHandler : IEventHandler<SyncResponseEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public SyncResponseEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(SyncResponseEvent @event)
    {
        var users = @event.AccountsCreatedOrUpdated.Select(u => {
            return new User()
            {
                Id = u.UserId,
                Name = u.UserName,
                LastUpdated = DateTime.UtcNow,
            };
        });
        await _unitOfWork.UsersRepo.AddOrUpdateBatchAsync(users);
        await _unitOfWork.SaveAsync();
    }
}
