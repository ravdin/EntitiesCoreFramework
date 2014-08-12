using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace EntitiesCoreFramework.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        /// <summary>
        /// Gets the value associated with the key.
        /// </summary>
        public T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }

        /// <summary>
        /// Adds a key and object to the cache.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="data">Data object.</param>
        /// <param name="cacheTime">Cache time, in minutes.</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Returns true if the key is set in the cache, false otherwise.
        /// </summary>
        public bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
