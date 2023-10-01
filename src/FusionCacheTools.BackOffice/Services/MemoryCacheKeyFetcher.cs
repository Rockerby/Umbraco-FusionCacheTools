using Microsoft.Extensions.Caching.Memory;

namespace FusionCacheTools.BackOffice.Services
{
    public class MemoryCacheKeyFetcher : ICacheKeyFetcher
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheKeyFetcher(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public IEnumerable<string> GetCacheKeys()
        {
            return _memoryCache.GetKeys<string>();
        }
    }
}
