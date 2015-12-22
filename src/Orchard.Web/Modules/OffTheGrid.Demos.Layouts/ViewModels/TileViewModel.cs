using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.MediaLibrary.Models;

namespace OffTheGrid.Demos.Layouts.ViewModels {
    public class TileViewModel {
        public string BackgroundImageId { get; set; }
        public string BackgroundSize { get; set; }
        public ImagePart BackgroundImage { get; set; }
    }
}