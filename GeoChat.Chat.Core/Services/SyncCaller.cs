using GeoChat.Chat.Core.EventBus;
using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.Repos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Services;

public class SyncCaller
{
    private readonly IEventBus _bus;
    private readonly IConfiguration _cfg;
    private readonly IUnitOfWork _unitOfWork;

    public SyncCaller(IEventBus bus, IConfiguration configuration, IUnitOfWork unitOfWork)
	{
		_bus = bus;
		_cfg = configuration;
		_unitOfWork = unitOfWork;
	}

	public async Task SyncAsync()
	{
		var date = await _unitOfWork.UsersRepo.GetLastUpdate();
        var baseCfg = $"RabbitMq:PublishRoutings:{nameof(SyncCallEvent)}";

        var exchange = _cfg[$"{baseCfg}:Exchange"];
        var exchangeType = _cfg[$"{baseCfg}:ExchangeType"];
		var routingKey = _cfg[$"{baseCfg}:RoutingKey"];
        if (exchange == null || exchangeType == null)
        {
            throw new Exception("The exchange or exchange type wasn't configured in the appsettings.json file.");
        }
        var @event = new SyncCallEvent()
		{
			LastSyncCall = date
		};
		_bus.PublishEvent(@event, exchange, exchangeType, routingKey);
	}
}
