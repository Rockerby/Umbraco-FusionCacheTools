using FusionCacheTools.BackOffice.Models;

namespace FusionCacheTools.BackOffice.Services
{
    public interface ICacheKeyFetcher
    {
        IEnumerable<FusionCachedObject> GetCacheKeys();
    }
}
