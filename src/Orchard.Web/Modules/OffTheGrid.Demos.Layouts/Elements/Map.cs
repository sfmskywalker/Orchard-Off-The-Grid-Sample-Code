using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Localization;

namespace OffTheGrid.Demos.Layouts.Elements {
    public class Map : Element {

        public override string Category => "Demo";
        public override LocalizedString Description => T("Renders a map with a specified location.");
        public override string ToolboxIcon => "\uf041;";

        public double Latitude {
            get { return this.Retrieve(x => x.Latitude); }
            set { this.Store(x => x.Latitude, value); }
        }

        public double Longitude {
            get { return this.Retrieve(x => x.Longitude); }
            set { this.Store(x => x.Longitude, value); }
        }
    }
}