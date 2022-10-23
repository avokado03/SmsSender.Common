using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SmsSender.Common.RabbitMQ.Interfaces;
using SmsSender.Common.RabbitMQ.Options;

namespace SmsSender.Common.RabbitMQ;

/// <summary>
/// Соединение с RabbitMq
/// </summary>
public class RabbitConnection : IRabbitConnection
{
    private ConnectionFactory? _factory = null;

    private IConnection? _connection = null;
    private readonly RabbitConnectionOptions _connectionOptions;

    /// <inheritdoc />
    public bool IsConnected => _connection != null;

    public RabbitConnection(IOptions<RabbitConnectionOptions> connectionOptions)
    {
        _connectionOptions = connectionOptions.Value 
            ?? throw new ArgumentNullException(nameof(RabbitConnectionOptions));
        CreateFactory();
    }

    private void CreateFactory()
    {
        _factory = new ConnectionFactory()
        {
            HostName = _connectionOptions.Host,
            UserName = _connectionOptions.User,
            Password = _connectionOptions.Password,
            Port = _connectionOptions.Port,
            Ssl =
            {
                ServerName = _connectionOptions.Host,
                Enabled = false
            }
        };
    }

    /// <inheritdoc />
    public void TryConnect()
    {
        try
        {
            if (_factory != null && !IsConnected)
            {
                _connection = _factory?.CreateConnection();
            }
        }
        catch (Exception e)
        {
            throw new Exception ("Rabbit exeption", e);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection?.Dispose();
    }

    /// <inheritdoc />
    public IModel CreateChannel()
    {
        return _connection!.CreateModel();
    }
}