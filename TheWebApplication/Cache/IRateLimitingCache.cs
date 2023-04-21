using System;
namespace Cache
{
	public interface IRateLimitingCache
	{
        bool AddToCache(string key, int expriryTimeInSeconds);
        bool IsInCache(string key);
        void RemoveFromCache(string key);
    }
}

