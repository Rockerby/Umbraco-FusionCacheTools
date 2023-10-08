using FusionCacheTools.BackOffice.Models;

namespace FusionCacheTools.BackOffice.Services
{
    public interface ICacheKeyFetcher
    {
        string Name { get; }
        IEnumerable<FusionCachedObject> GetCacheKeys();
    }
}
