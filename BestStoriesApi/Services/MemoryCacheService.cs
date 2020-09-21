using Microsoft.Extensions.Caching.Memory;
using System;

namespace BestStoriesApi.Services
{
    public class MemoryCacheService : ICacheService
    {
        
        protected readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T o))
            {
                return o;
            }

            return default;
        }

        public void Set<T>(string key, T o, TimeSpan absoluteExpiration)
        {           
            _cache.Set(key, o, absoluteExpiration);
        }
    }
}
