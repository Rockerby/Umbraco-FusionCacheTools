using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using FusionCacheTools.BackOffice.Models;
using FusionCacheTools.BackOffice.Services;
using NPoco;
using System;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Website.Controllers;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Events;
using ZiggyCreatures.Caching.Fusion.Plugins;
using static Azure.Core.HttpHeader;
using FusionCacheTools.BackOffice.FusionCacheHelpers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace FusionCacheTools.BackOffice.Controllers
{
    [PluginController("FusionCacheTools")]
    public class CacheController : UmbracoAuthorizedApiController
    {
        public static List<string> AddedKeys = new List<string>();

        private readonly IFusionCache _cache;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        private readonly IScopeProvider _scopeProvider;
        private readonly IConfiguration _configuration;
        private readonly FusionCachePlugin _cachePlugin;
        private readonly ICacheKeyPersistenceService _cacheKeyPersistenceService;

        public CacheController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider, IFusionCache cache,
            IServiceProvider serviceProvider, IEnumerable<IFusionCachePlugin> cachePlugins,
            IDistributedCache distributedCache, IScopeProvider scopeProvider,
            IConfiguration configuration, IMemoryCache memoryCache,
            ICacheKeyPersistenceService cacheKeyPersistenceService) : base()
        {
            _cache = cache;
            _serviceProvider = serviceProvider;
            _distributedCache = distributedCache;
            _scopeProvider = scopeProvider;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _cacheKeyPersistenceService = cacheKeyPersistenceService;

            // This needs to be an enumerable as we may have > 1 singleton of IFusionCachePlugin
            foreach (IFusionCachePlugin plugin in cachePlugins)
            {
                if (plugin is FusionCachePlugin)
                {
                    _cachePlugin = plugin as FusionCachePlugin;
                    break;
                }
            }
        }

        public bool AddCache(string id)
        {
            var product = _cache.GetOrSet<TestProd>(
                $"product:{id}",
                _ => new TestProd() { ID = id },
                options => options.SetDuration(TimeSpan.FromMinutes(3600))
            );

            return true;
        }
        private IEnumerable<string> GetDistKeys()
        {
            // This only works if the distributed cache is on the same database as the Umbraco instance...
            //using var scope = _scopeProvider.CreateScope();
            //var data = scope.Database.Fetch<SqlCacheItem>(new Sql("select Id from CustomCache"));
            //scope.Complete();

            return Enumerable.Empty<string>();
        }


        public object GetCacheItem(string key)
        {
            var product = _cache.GetOrDefault<object>(key);

            return product;
        }

        public bool RemoveCacheItem(string key)
        {
            _cache.Remove(key);

            return true;
        }

        public IEnumerable<FusionCachedObject> GetAllCacheKeys()
        {

            //var allCachedObjects = _cachePlugin.GetAllCachedObjects();
            //return allCachedObjects ?? Enumerable.Empty<FusionCachedObject>();
            return _cacheKeyPersistenceService.GetCacheKeys();

            //IEnumerable<string> objs = _cachePlugin.GetAllCachedObjects();
            //IEnumerable<string> objs2 = GetDistKeys();
            //var coll = _memoryCache.GetKeys<string>();

            //var combinedKeys = objs.Union(objs2).Union(coll);
            //foreach (string key in combinedKeys)
            //{
            //    var cacheObj = _cache.GetOrDefault<object>(GetNormalisedCacheKey(key));
            //}

            //return objs.Union(objs2);
        }
    }

    public class TestProd
    {
        public string ID { get; set; }
        public override string ToString()
        {
            return base.ToString() + " -- " + ID;
        }
    }
}
