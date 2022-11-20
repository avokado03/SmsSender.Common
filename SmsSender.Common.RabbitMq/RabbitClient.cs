using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmsSender.Common.RabbitMQ.Interfaces;
using SmsSender.Common.RabbitMQ.Options;

namespace SmsSender.Common.RabbitMQ;

/// <summary>
/// Клиент шины Rabbit
/// </summary>
public class RabbitClient : IRabbitClient
{
    private readonly IRabbitConnection _connection;
    private readonly RabbitQueueOptions _queueOptions;

    public RabbitClient(IOptions<RabbitQueueOptions> queueOptions, IRabbitConnection connection)
    {       
        _queueOptions = queueOptions.Value 
            ?? throw new ArgumentNullException(nameof(RabbitQueueOptions));
        _connection = connection;
    }

    private void Publish (string message, RabbitQueueOption queueOptions)
    {
        if (!_connection.IsConnected)
        {
            _connection.TryConnect();
        }
        using var channel = _connection.CreateChannel();
        DeclareQueue(channel, queueOptions);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty, 
            routingKey: queueOptions.Name,
            basicProperties: null,
            body: body);
    }

    public void Publish(string message, string queueName)
    {
        Publish(message, _queueOptions.QueueOptions[queueName]);
    }

    /// <inheritdoc />
    public void Publish<T>(T model, string queueName)
        where T : IMessage
    {
        string message = JsonSerializer.Serialize(model);
        Publish(message, queueName);
    }

    private void Subscribe(EventHandler<BasicDeliverEventArgs> received, RabbitQueueOption queueOption)
    {
        if (!_connection.IsConnected)
        {
            _connection.TryConnect();
        }
        using var channel = _connection.CreateChannel();
        DeclareQueue(channel, queueOption);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += received;
        channel.BasicConsume(queue: queueOption.Name,
            autoAck: queueOption.AutoAck,
            consumer: consumer);
    }

    /// <inheritdoc />
    public void Subscribe (EventHandler<BasicDeliverEventArgs> received, string queueName)
    {
        Subscribe(received, _queueOptions.QueueOptions[queueName]);
    }

    /// <inheritdoc />
    public void Subscribe<T>(Action<T, object, BasicDeliverEventArgs> handler, string queueName)
        where T : IMessage
    {       
        Subscribe((model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var dto = JsonSerializer.Deserialize<T>(message);
            handler(dto!, model!, ea);
        }, queueName);
    }

    private void DeclareQueue(IModel channel, RabbitQueueOption queueOption)
    {
        channel.QueueDeclare(
            queue: queueOption.Name,
            durable: queueOption.Durable,
            exclusive: queueOption.Exclusive,
            autoDelete: queueOption.AutoDelete,
            arguments: queueOption.Arguments);
    }
}