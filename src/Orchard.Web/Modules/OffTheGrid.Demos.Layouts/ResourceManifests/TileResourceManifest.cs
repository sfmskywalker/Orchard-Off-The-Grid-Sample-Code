using Orchard.UI.Resources;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class TileResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("TileElement").SetUrl("TileElement.min.js", "TileElement.js").SetDependencies("Layouts.LayoutEditor");
        }
    }
}