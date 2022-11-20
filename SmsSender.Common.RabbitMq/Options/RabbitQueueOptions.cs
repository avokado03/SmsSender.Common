namespace SmsSender.Common.RabbitMQ.Options;

/// <summary>
/// Конфигурация для набора очередей сервиса
/// </summary>
public class RabbitQueueOptions
{
    /// <summary>
    /// Набор очередей
    /// </summary>
    public Dictionary<string, RabbitQueueOption> QueueOptions { get; set; }
}
