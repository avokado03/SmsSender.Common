namespace SmsSender.Common.RabbitMQ.Options;

/// <summary>
/// Конфигурация подключения к Rabbit
/// </summary>
public class RabbitConnectionOptions
{
    /// <summary>
    /// Url
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Порт
    /// </summary>
    public int Port { get; set; }
}
