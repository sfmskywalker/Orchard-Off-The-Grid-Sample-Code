using Orchard.Layouts.ViewModels;

namespace OffTheGrid.Demos.Layouts.ViewModels {
    public class SlideEditorViewModel {
        public string Session { get; set; }
        public int? SlideIndex { get; set; }
        public LayoutEditor LayoutEditor { get; set; }
    }
}