using StackExchange.Redis;
using Microsoft.Extensions.Options;

namespace SmsSender.Common.Redis
{
    /// <summary>
    /// Сервис RateLimiter (реализация с ограничением по времени)
    /// </summary>
    public class RedisRateLimiter
    {
        private ConnectionMultiplexer _connection;
        private IDatabase _db;

        public RedisRateLimiter(IOptions<RedisOptions> options)
        {
            _connection = ConnectionMultiplexer.Connect(options.Value.Connection);
            _db = _connection.GetDatabase();

        }

        /// <summary>
        /// Метод проверки на превышение лимита операций на промежуток времени
        /// </summary>
        /// <param name="key">Хэш-ключ Redis-значения лимита</param>
        /// <param name="allowCountPerTime">На сколько нужно ограничить значение переменной</param>
        /// <param name="lifetime">На протяжении какого времени эта переменная живет</param>
        /// <returns>Результат проверки</returns>
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