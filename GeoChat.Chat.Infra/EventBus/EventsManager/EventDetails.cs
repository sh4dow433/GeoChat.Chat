﻿namespace GeoChat.Chat.Infra.EventBus.EventsManager;

public record EventDetails(string Exchange,
        string ExchangeType,
        string Queue,
        string RoutingKey,
        bool AutoDelete = false);
