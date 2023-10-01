using FusionCacheTools.BackOffice.Models;

namespace FusionCacheTools.BackOffice.Services
{
    public interface ICacheKeyPersistenceService
    {
        IEnumerable<FusionCachedObject> GetCacheKeys();
    }
}
