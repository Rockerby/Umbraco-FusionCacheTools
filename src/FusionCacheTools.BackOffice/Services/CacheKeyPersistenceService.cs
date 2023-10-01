using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FusionCacheTools.BackOffice.Services
{
    public class CacheKeyPersistenceService : ICacheKeyPersistenceService
    {
        private readonly IEnumerable<ICacheKeyFetcher> _cacheKeyFetchers;
        private readonly ILogger<CacheKeyPersistenceService> _logger;
        public CacheKeyPersistenceService(IEnumerable<ICacheKeyFetcher> cacheKeyFetchers, ILogger<CacheKeyPersistenceService> logger)
        {
            _cacheKeyFetchers = cacheKeyFetchers;
            _logger = logger;
        }

        /// <summary>
        /// Fetch the keys from all the registered ICacheKeyFetchers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetCacheKeys()
        {
            if (_cacheKeyFetchers == null || !_cacheKeyFetchers.Any())
                throw new ArgumentException("No implementations of ICacheKeyFetcher found. Have you registered one?");

            IEnumerable<string> retData = Enumerable.Empty<string>();

            foreach (var cacheKey in _cacheKeyFetchers)
            {
                try
                {
                    retData = retData.Union(cacheKey.GetCacheKeys());
                }
                catch(Exception ex)
                {
                    _logger.LogError("Unable to fetch keys from " + cacheKey.GetType(), ex);
                }
            }

            return retData.Distinct();
        }
    }
}
