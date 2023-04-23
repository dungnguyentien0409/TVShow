using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Cache
{
	public class RateLimitingCache : IRateLimitingCache
    {
        private IMemoryCache _memoryCache;

        public RateLimitingCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public bool AddToCache(string key, int expire)
        {
            bool isSuccess = false; 

            if (!IsInCache(key))
            {
                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(expire));
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .AddExpirationToken(new CancellationChangeToken(cancellationTokenSource.Token));

                _memoryCache.Set(key, DateTime.Now, cacheEntryOptions);

                isSuccess = true;
            }

            return isSuccess;
        }

        public bool IsInCache(string key)
        {
            var item = _memoryCache.Get(key);

            return item != null;
        }

        public void RemoveFromCache(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}

