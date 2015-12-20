﻿using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment;
using Orchard.UI.Resources;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class TileResources : IShapeTableProvider {
        private readonly Work<IResourceManager> _resourceManager;
        public TileResources(Work<IResourceManager> resourceManager) {
            _resourceManager = resourceManager;
        }

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("EditorTemplate").OnDisplaying(context => {
                if (context.Shape.TemplateName != "Parts.Layout")
                    return;

                _resourceManager.Value.Require("script", "TileElement");
            });
        }
    }
}