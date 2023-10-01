using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Manifest;

namespace FusionCacheTools.BackOffice
{
    public class ManifestComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.ManifestFilters().Append<ManifestFilter>();
        }
    }

    internal class ManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            manifests.Add(new PackageManifest
            {
                PackageName = "Fusion Cache Tools",
                //Dashboards = new[] { new ManifestDashboard {
                //    Alias = "spektrix",
                //    View = "/App_Plugins/SpektrixHelper/Dashboard_Settings.html",
                //    Sections = new [] { "settings" },
                //    Weight = 5,
                //    AccessRules = new Umbraco.Cms.Core.Dashboards.IAccessRule [] {
                //        new Umbraco.Cms.Core.Dashboards.AccessRule {
                //            Type = AccessRuleType.Grant,
                //            Value = "admin"
                //        }
                //    }
                //}
                //},
                Scripts = new[]
                {
                    "/App_Plugins/FusionCacheTools/scripts/fusionCacheTools.resources.js",
                    "/App_Plugins/FusionCacheTools/scripts/settingsCacheDashboard.controller.js",
                },
                Stylesheets = new[]
                {
                    "/App_Plugins/FusionCacheTools/css/main.css"
                },
                Version = typeof(ManifestFilter).Assembly.GetName().Version.ToString(3),
            });
        }
    }
}