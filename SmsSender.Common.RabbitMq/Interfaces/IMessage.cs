namespace SmsSender.Common.RabbitMQ.Interfaces;

/// <summary>
/// Контракт для сообщений шины
/// </summary>
public interface IMessage
{
}

/// <summary>
/// Контракт для команд, передаваемых по шине
/// </summary>
public interface ICommand : IMessage
{
}

/// <summary>
/// Контракт для событий, передаваемых по шине
/// </summary>
public interface IEvent : IMessage
{
}
