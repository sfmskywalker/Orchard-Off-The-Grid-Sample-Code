using Newtonsoft.Json.Linq;
using OffTheGrid.Demos.Layouts.Elements;
using Orchard.ContentManagement;
using Orchard.Layouts.Services;
using Orchard.MediaLibrary.Models;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class TileModelMap : LayoutModelMapBase<Tile> {
        private IContentManager _contentManager;

        public TileModelMap(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public override void FromElement(Tile element, DescribeElementsContext describeContext, JToken node) {
            base.FromElement(element, describeContext, node);

            var backgroundImage = element.BackgroundImageId != null 
                ? _contentManager.Get<ImagePart>(element.BackgroundImageId.Value)
                : default(ImagePart);
            var backgroundImageUrl = backgroundImage?.As<MediaPart>().MediaUrl;

            node["backgroundUrl"] = backgroundImageUrl;
            node["backgroundSize"] = element.BackgroundSize;
        }

        protected override void ToElement(Tile element, JToken node) {
            base.ToElement(element, node);

            element.BackgroundSize = (string)node["backgroundSize"];
        }
    }
}