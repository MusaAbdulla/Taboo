using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Taboo.External_Services.Abstracts;

namespace Taboo.External_Services.Implements
{
    public class RedisService(IDistributedCache _redis) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key)
        {
            string? data= await _redis.GetStringAsync(key);
            if (data == null) return default(T?);
            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetAsync<T>(string key, T data,int seconds=30)
        {
           string json =JsonSerializer.Serialize(data);
          DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpirationRelativeToNow=TimeSpan.FromSeconds(30);
            await _redis.SetStringAsync(key, json,opt);
        }
    }
}
