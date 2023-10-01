using FusionCacheTools.BackOffice.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FusionCacheTools.BackOffice.Services
{
    public class MemoryCacheKeyFetcher : ICacheKeyFetcher
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheKeyFetcher(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public IEnumerable<FusionCachedObject> GetCacheKeys()
        {
            foreach (string key in _memoryCache.GetKeys<string>()) {
                yield return new FusionCachedObject()
                {
                    Key = key,
                    Expiration = DateTime.UtcNow, //TODO: How do we get this!
                };
            };                
        }
    }
}
