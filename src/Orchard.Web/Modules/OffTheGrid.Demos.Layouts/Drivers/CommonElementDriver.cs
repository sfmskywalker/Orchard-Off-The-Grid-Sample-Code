//using OffTheGrid.Demos.Layouts.Helpers;
//using OffTheGrid.Demos.Layouts.ViewModels;
//using Orchard.Layouts.Framework.Display;
//using Orchard.Layouts.Framework.Drivers;
//using Orchard.Layouts.Framework.Elements;
//using Orchard.UI.Resources;

//namespace OffTheGrid.Demos.Layouts.Elements {
//    public class CommonElementDriver : ElementDriver<Element> {
//        private readonly IResourceManager _resourceManager;

//        public CommonElementDriver(IResourceManager resourceManager) {
//            _resourceManager = resourceManager;
//        }

//        protected override EditorResult OnBuildEditor(Element element, ElementEditorContext context) {
//            // Initialize the view model with existing data.
//            var viewModel = new ElementViewModel {
//                FadeIn = element.GetFadeIn()
//            };
            
//            // Model bind the view model if an Updater is provided.
//            if(context.Updater != null) {
//                if (context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null)) {
//                    element.SetFadeIn(viewModel.FadeIn);
//                }
//            }

//            // Create the editor template shape.
//            var visibilityEditorTemplate = context.ShapeFactory.EditorTemplate(
//                TemplateName: "Elements/Common.Visibility",
//                Model: viewModel,
//                Prefix: context.Prefix);

//            // Specify the position of the editor shapes.
//            // This is key to assigning editor templates to a tab.
//            visibilityEditorTemplate.Metadata.Position = "Visibility:1";

//            // Return the editor shape.
//            return Editor(context, visibilityEditorTemplate);
//        }

//        protected override void OnDisplaying(Element element, ElementDisplayingContext context) {
//            if (context.DisplayType == "Design")
//                return;

//            if (!element.GetFadeIn())
//                return;

//            context.ElementShape.Classes.Add("auto-fade-in");
//            _resourceManager.Require("stylesheet", "CommonElement");
//        }
//    }
//}