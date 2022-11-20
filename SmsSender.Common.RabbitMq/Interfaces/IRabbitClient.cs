using RabbitMQ.Client.Events;

namespace SmsSender.Common.RabbitMQ.Interfaces;

/// <summary>
/// Контракт для клиента шины Rabbit
/// </summary>
public interface IRabbitClient
{
    /// <summary>
    /// Публикует строковое сообщение
    /// </summary>
    void Publish(string message, string queueName);

    /// <summary>
    /// Публикует сообщение
    /// </summary>
    /// <typeparam name="T">Тип сообщения</typeparam>
    void Publish<T>(T model, string queueName) where T : IMessage;

    /// <summary>
    /// Подписка на получение сообщений в строковом формате
    /// из очереди
    /// </summary>
    void Subscribe(EventHandler<BasicDeliverEventArgs> received, string queueName);

    /// <summary>
    /// Подписка на получение сообщений определенного типа
    /// (с десериализацией)
    /// </summary>
    /// <typeparam name="T">Тип, в который необходимо десериализовать</typeparam>
    void Subscribe<T>(Action<T, object, BasicDeliverEventArgs> handler, string queueName)
        where T : IMessage;
}
