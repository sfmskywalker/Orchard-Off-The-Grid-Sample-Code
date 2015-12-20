using System;
using Newtonsoft.Json.Linq;
using OffTheGrid.Demos.Layouts.Elements;
using Orchard.Layouts.Services;
using Orchard.Utility.Extensions;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class TileModelMap : LayoutModelMapBase<Tile> {
        public override void FromElement(Tile element, DescribeElementsContext describeContext, JToken node) {
            base.FromElement(element, describeContext, node);
            node["hasEditor"] = element.HasEditor;
            node["contentType"] = element.Descriptor.TypeName;
            node["contentTypeLabel"] = element.Descriptor.DisplayText.Text;
            node["contentTypeClass"] = String.Format(element.DisplayText.Text.HtmlClassify());
        }
    }
}