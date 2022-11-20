namespace SmsSender.Common.Redis;

/// <summary>
/// Контракт для сервисов Redis для проверки лимитов операций
/// </summary>
public interface IRedisRateLimiter
{
    /// <summary>
    /// Метод проверки на превышение лимита операций на промежуток времени
    /// </summary>
    /// <param name="key">Хэш-ключ Redis-значения лимита</param>
    /// <param name="allowCountPerTime">На сколько нужно ограничить значение переменной</param>
    /// <param name="lifetime">На протяжении какого времени эта переменная живет</param>
    /// <returns>Результат проверки</returns>
    bool CheckLimit(string key, int allowCountPerTime, TimeSpan lifetime);
}
