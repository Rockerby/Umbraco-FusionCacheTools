using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Community.FusionCacheTools
{
    internal class FusionCacheToolsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.ManifestFilters().Append<FusionCacheToolsManifestFilter>();
        }
    }
}
