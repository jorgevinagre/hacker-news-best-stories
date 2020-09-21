using System;

namespace BestStoriesApi.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Adds a new item to cache with absolute expiration
        /// </summary>
        /// <typeparam name="T">Type to add to cache.</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="o">Instance to add to to cache</param>
        /// <param name="absoluteExpiration">Absolute expiration offset</param>
        void Set<T>(string key, T o, TimeSpan absoluteExpiration);       
        /// <summary>
        /// Retrieves a single value from cache, or the default type instance, if not found.
        /// </summary>
        /// <typeparam name="T">Instance to add to to cache</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Key value or the default of T</returns>
        T Get<T>(string key);
    }
}
