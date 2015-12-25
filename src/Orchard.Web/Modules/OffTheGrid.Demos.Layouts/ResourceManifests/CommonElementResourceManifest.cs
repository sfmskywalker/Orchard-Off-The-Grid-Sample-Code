using Orchard.UI.Resources;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class CommonElementResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("CommonElement").SetUrl("CommonElement.min.css", "CommonElement.css");
        }
    }
}