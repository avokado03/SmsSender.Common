using StackExchange.Redis;
using Microsoft.Extensions.Options;

namespace SmsSender.Common.Redis
{
    /// <summary>
    /// Сервис RateLimiter (реализация с ограничением по времени)
    /// </summary>
    public class RedisRateLimiter : IRedisRateLimiter
    {
        private ConnectionMultiplexer _connection;
        private IDatabase _db;

        public RedisRateLimiter(IOptions<RedisOptions> options)
        {
            _connection = ConnectionMultiplexer.Connect(options.Value.Connection);
            _db = _connection.GetDatabase();
        }

        /// <inheritdoc />
        public bool CheckLimit(string key, int allowCountPerTime, TimeSpan lifetime)
        {
            if (_db.StringGet(key) == RedisValue.Null)
            {
                _db.StringSet(key, 0, lifetime, true);
            }

            int count = Convert.ToInt32(_db.StringGet(key));

            bool allow = count < allowCountPerTime;

            _db.StringIncrement(key, flags: CommandFlags.FireAndForget);

            return allow;
        }
    }
}