namespace SmsSender.Common.RabbitMQ.Options;

/// <summary>
/// Опции очереди RabbitMq
/// </summary>
public class RabbitQueueOption
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Признак устойчивости
    /// </summary>
    public bool Durable { get; set; } = false;

    /// <summary>
    /// Признак разрешения подключения
    /// единственного consumer
    /// </summary>
    public bool Exclusive { get; set; } = false;

    /// <summary>
    /// Признак самоуничтожения, когда нет потребителей
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// Признак автоматического подтверждения
    /// при получении сообщения
    /// </summary>
    public bool AutoAck { get; set; } = true;

    /// <summary>
    /// Дополнительные аргументы
    /// </summary>
    public IDictionary<string, object>? Arguments { get; set; }
}