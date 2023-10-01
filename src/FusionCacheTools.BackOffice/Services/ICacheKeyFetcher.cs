namespace FusionCacheTools.BackOffice.Services
{
    public interface ICacheKeyFetcher
    {
        IEnumerable<string> GetCacheKeys();
    }
}
