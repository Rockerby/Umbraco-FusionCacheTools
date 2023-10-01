using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Core.DependencyInjection;
using FusionCacheTools.BackOffice.Services;
using Microsoft.Extensions.DependencyInjection;
using FusionCacheTools.BackOffice.FusionCacheHelpers;
using ZiggyCreatures.Caching.Fusion.Plugins;
using ZiggyCreatures.Caching.Fusion;

namespace FusionCacheTools.BackOffice.Composers
{
    public class MainComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //TODO: Add support for setting up SQL

            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = _config.GetConnectionString("umbracoDbDSN");
            //    options.SchemaName = "dbo";
            //    options.TableName = "CustomCache";
            //});
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = _config.GetConnectionString("cacheDb");
            //    options.SchemaName = "dbo";
            //    options.TableName = "CustomCache";
            //});

            SetupFusionCache(builder);
            AddFusionCacheTools(builder);
        }

        /// <summary>
        /// Sets up the core Fusion Cache, defaulting to memory cache
        /// If the cache is already setup then plugins will be registered
        /// </summary>
        /// <param name="builder"></param>
        private void SetupFusionCache(IUmbracoBuilder builder)
        {
            //TODO: Implement this functionality

            // If (fusion cache is not registered {
            builder.Services.AddFusionCacheNewtonsoftJsonSerializer();
            builder.Services.AddSingleton<IFusionCachePlugin, FusionCachePlugin>();
            builder.Services.AddFusionCache().WithAllRegisteredPlugins().TryWithAutoSetup();
            // } else {
            // Just add plugins
            //}
        }

        private void AddFusionCacheTools(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<ICacheKeyFetcher, MemoryCacheKeyFetcher>();
            //builder.Services.AddSingleton<ICacheKeyFetcher, SqlDistCacheKeyFetcher>();
            builder.Services.AddSingleton<ICacheKeyPersistenceService, CacheKeyPersistenceService>();
        }
    }
}
