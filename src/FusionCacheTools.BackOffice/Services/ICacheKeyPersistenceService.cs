namespace FusionCacheTools.BackOffice.Services
{
    public interface ICacheKeyPersistenceService
    {
        IEnumerable<string> GetCacheKeys();
    }
}
