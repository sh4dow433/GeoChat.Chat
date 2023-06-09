﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace GeoChat.Chat.Infra.EventBus.ConnectionManager;

public partial class RabbitMqConnectionManager : IRabbitMqConnectionManager
{

    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private static readonly Dictionary<string, EventingBasicConsumer> _registeredConsumers = new();

    public event AsyncEventHandler<MessageEventArgs>? EventReceived;


    public IModel Channel => _channel;

    public RabbitMqConnectionManager(IConfiguration configuration)
    {
        _configuration = configuration;

        var host = _configuration["RabbitMq:Host"];
        var port = int.Parse(_configuration["RabbitMq:Port"] ?? "5762");
        var vhost = _configuration["RabbitMq:Vhost"];
        var userName = _configuration["RabbitMq:UserName"] ?? "guest";
        var password = _configuration["RabbitMq:Password"] ?? "guest";

        var factory = new ConnectionFactory()
        {
            UserName = userName,
            Password = password,
            HostName = host,
            VirtualHost= vhost,
            Port = port
        };
        _connection = factory.CreateConnection();
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        _channel = _connection.CreateModel();
        _channel.CallbackException += (chann, args) =>
        {
            Console.WriteLine(args.Exception);
        };
    }


    public string StartConsumer(string exchange, string exchangeType, string queue, string routing, bool autodelete = false)
    {
        _channel.ExchangeDeclare(exchange: exchange, type: exchangeType);

        _channel.QueueDeclare(queue, false, false, autodelete, null);
        _channel.QueueBind(queue: queue,
                          exchange: exchange,
                          routingKey: routing);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnReceived;

        var consumerTag = _channel.BasicConsume(queue: queue,
                             autoAck: true,
                             consumer: consumer);

        _registeredConsumers[consumerTag] = consumer;
        Console.WriteLine("---> Consumer Registered");
        return consumerTag;
    }

    public void StopConsumer(string consumerTag)
    {
        var consumer = _registeredConsumers[consumerTag];
        if (consumer == null)
        {
            return;
        }
        _channel.BasicCancel(consumerTag);
        consumer.Received -= OnReceived;
        _registeredConsumers.Remove(consumerTag);

        Console.WriteLine("---> Consumer Stopped");
    }

    private void OnReceived(object? model, BasicDeliverEventArgs ea)
    {
        Console.WriteLine("---> Message received");
        var routingKey = ea.RoutingKey;
        var exchange = ea.Exchange;
        byte[] body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        if (EventReceived == null)
        {
            throw new Exception("No event processor was added to the OnReceived event");
        }
        EventReceived
            .Invoke(this, new(message, routingKey, exchange))
            .GetAwaiter()
            .GetResult();
    }


    public void Dispose()
    {
        _connection.ConnectionShutdown -= RabbitMQ_ConnectionShutdown;

        _channel.Close();
        _connection.Close();

        _channel.Dispose();
        _connection.Dispose();

        Console.WriteLine("---> Connection Disposed");
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("---> RabbitMQ Connection Shutdown");
    }
}