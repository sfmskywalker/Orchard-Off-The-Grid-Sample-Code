using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace OffTheGrid.Demos.Layouts.Elements {
    public class SlideShow : Element {
        public override string Category => "Media";
        public override string ToolboxIcon => "\uf03e";

        public int Interval {
            get { return this.Retrieve(x => x.Interval, () => 3000); }
            set { this.Store(x => x.Interval, value); }
        }

        public bool Controls {
            get { return this.Retrieve(x => x.Controls, () => true); }
            set { this.Store(x => x.Controls, value); }
        }

        public bool Indicators {
            get { return this.Retrieve(x => x.Indicators, () => true); }
            set { this.Store(x => x.Indicators, value); }
        }

        public string Pause {
            get { return this.Retrieve(x => x.Pause); }
            set { this.Store(x => x.Pause, value); }
        }

        public bool Wrap {
            get { return this.Retrieve(x => x.Wrap); }
            set { this.Store(x => x.Wrap, value); }
        }

        public bool Keyboard {
            get { return this.Retrieve(x => x.Keyboard); }
            set { this.Store(x => x.Keyboard, value); }
        }

        public string SlidesData {
            get { return this.Retrieve(x => x.SlidesData); }
            set { this.Store(x => x.SlidesData, value); }
        }
    }
}