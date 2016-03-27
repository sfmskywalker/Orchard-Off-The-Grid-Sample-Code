using System;
using System.Collections.Generic;
using OffTheGrid.Demos.Layouts.Helpers;
using OffTheGrid.Demos.Layouts.ViewModels;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Services;
using Orchard.UI.Resources;

namespace OffTheGrid.Demos.Layouts.Handlers {
    public class CommonElementHandler : ElementEventHandlerBase {
        private readonly IResourceManager _resourceManager;

        public CommonElementHandler(IResourceManager resourceManager) {
            _resourceManager = resourceManager;
        }

        private readonly IList<Type> SupportedTypes = new List<Type> {
            { typeof(Html) },
            { typeof(Image) },
        };

        public override void BuildEditor(ElementEditorContext context) {
            var element = context.Element;
            var elementType = element.GetType();

            if (!SupportedTypes.Contains(elementType))
                return;
            
            // Initialize the view model with existing data.
            var viewModel = new ElementViewModel {
                FadeIn = element.GetFadeIn()
            };

            // Model bind the view model if an Updater is provided.
            if (context.Updater != null) {
                if (context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null)) {
                    element.SetFadeIn(viewModel.FadeIn);
                }
            }

            // Create the editor template shape.
            var visibilityEditorTemplate = context.ShapeFactory.EditorTemplate(
                TemplateName: "Elements/Common.Visibility",
                Model: viewModel,
                Prefix: context.Prefix);

            // Specify the position of the editor shapes.
            // This is key to assigning editor templates to a tab.
            visibilityEditorTemplate.Metadata.Position = "Visibility:1";

            // Add the editor shape.
            context.EditorResult.Add(visibilityEditorTemplate);
        }

        public override void Displaying(ElementDisplayingContext context) {
            if (context.DisplayType == "Design")
                return;

            var element = context.Element;
            var elementType = element.GetType();

            if (!SupportedTypes.Contains(elementType))
                return;

            if (!element.GetFadeIn())
                return;

            context.ElementShape.Classes.Add("auto-fade-in");
            _resourceManager.Require("stylesheet", "CommonElement");
        }
    }
}