using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.FusionCacheTools
{
    internal class FusionCacheToolsManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(FusionCacheToolsManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "FusionCacheTools",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true
            });
        }
    }
}
