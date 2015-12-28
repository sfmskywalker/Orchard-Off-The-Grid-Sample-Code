using System.Collections.Generic;
using OffTheGrid.Demos.Layouts.Elements;

namespace OffTheGrid.Demos.Layouts.ViewModels {
    public class SlideShowViewModel {
        public SlideShow Element { get; set; }
        public string Session { get; set; }
        public IList<dynamic> Slides { get; set; }
        public IList<int> Indices { get; set; }
        public string SlidesData { get; set; }
        public int Interval { get; set; }
        public bool Controls { get; set; }
        public bool Indicators { get; set; }
        public string Pause { get; set; }
        public bool Wrap { get; set; }
        public bool Keyboard { get; set; }
    }
}