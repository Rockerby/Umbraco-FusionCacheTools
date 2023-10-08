using FusionCacheTools.BackOffice.Models;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;
using ZiggyCreatures.Caching.Fusion.Plugins;

namespace FusionCacheTools.BackOffice.FusionCacheHelpers
{
    public class FusionCachePlugin : IFusionCachePlugin
    {
        private IFusionCache fusionCache;

        public static List<string> AddedKeys = new List<string>();
        EventHandler<FusionCacheEntryEventArgs> set = (s, e) =>
        {
            int i = 0;
            if (!AddedKeys.Contains(e.Key))
                AddedKeys.Add(e.Key);
        };

        EventHandler<FusionCacheEntryHitEventArgs> hit = (s, e) =>
        {
            int i = 0;
            if (!AddedKeys.Contains(e.Key))
                AddedKeys.Add(e.Key);
        };

        EventHandler<FusionCacheEntryEventArgs> remove = (s, e) =>
        {
            int i = 0;
            AddedKeys.Remove(e.Key);
        };


        public void Start(IFusionCache cache)
        {
            fusionCache = cache;

            //This works fine for a memory cache as the static list is persistent with the cache
            cache.Events.Hit += hit;
            cache.Events.Set += set;
            cache.Events.Remove += remove;

            //HOW do we get a list of all cached items? Use the event to store them in the database so we can fetch them on boot?
            //cache.Events.Distributed.Set += set;
        }

        public void Stop(IFusionCache cache)
        {
            cache.Events.Set -= set;
            cache.Events.Hit -= hit;
            cache.Events.Remove -= remove;
            //cache.Events.Distributed.Set -= set;
        }

        public IEnumerable<FusionCachedObject> GetAllCachedObjects()
        {
            foreach (string key in AddedKeys)
            {
                var x = fusionCache.GetOrDefault<object>(key);
                yield return new FusionCachedObject()
                {
                    Key = key,
                    Expiration = DateTime.Now,
                    //Data = fusionCache.GetOrDefault<object>(key).ToString();
                };
            }
        }
    }
}
