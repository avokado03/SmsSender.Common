using RabbitMQ.Client;

namespace SmsSender.Common.RabbitMQ.Interfaces;

/// <summary>
/// Контракт для соединения с шиной Rabbit
/// </summary>
public interface IRabbitConnection : IDisposable
{
    /// <summary>
    /// Признак соединения
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Пробует осуществить соединение
    /// </summary>
    void TryConnect();

    /// <summary>
    /// Создает канал для соединения с шиной
    /// </summary>
    IModel CreateChannel();
}
