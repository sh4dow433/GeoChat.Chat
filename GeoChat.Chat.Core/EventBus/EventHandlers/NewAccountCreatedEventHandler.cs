using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.Models;
using GeoChat.Chat.Core.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.EventBus.EventHandlers;

public class NewAccountCreatedEventHandler : IEventHandler<NewAccountCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public NewAccountCreatedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(NewAccountCreatedEvent @event)
    {
        var user = new User()
        {
            Id = @event.UserId,
            Name = @event.UserName
        };
        _unitOfWork.UsersRepo.Create(user);
        await _unitOfWork.SaveAsync();
    }
}
