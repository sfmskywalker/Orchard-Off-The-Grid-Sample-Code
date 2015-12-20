using System.Collections.Generic;
using Orchard.ContentManagement;

namespace OffTheGrid.Demos.Layouts.ViewModels {
    public class TileViewModel {
        public string BackgroundImageId { get; set; }
        public IList<ContentItem> BackgroundImages { get; set; }
    }
}