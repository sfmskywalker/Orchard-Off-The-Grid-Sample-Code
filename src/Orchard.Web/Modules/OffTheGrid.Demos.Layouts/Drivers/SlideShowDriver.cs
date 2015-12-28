using Orchard.Layouts.Framework.Drivers;
using OffTheGrid.Demos.Layouts.ViewModels;
using Orchard.Layouts.Framework.Display;
using System.Collections.Generic;
using OffTheGrid.Demos.Layouts.Models;
using System.Linq;
using Orchard.Layouts.Services;

namespace OffTheGrid.Demos.Layouts.Elements {
    public class SlideshowElementDriver : ElementDriver<SlideShow> {
        private ILayoutManager _layoutManager;
        private ISlidesSerializer _slidesSerializer;

        public SlideshowElementDriver(ILayoutManager layoutManager, ISlidesSerializer slidesSerializer) {
            _layoutManager = layoutManager;
            _slidesSerializer = slidesSerializer;
        }

        protected override EditorResult OnBuildEditor(SlideShow element, ElementEditorContext context) {
            var slides = RetrieveSlides(element).ToList();
            var slideShapes = slides.Select(x => _layoutManager.RenderLayout(x.LayoutData, content: context.Content)).ToList();
            var viewModel = new SlideShowViewModel {
                Element = element,
                Session = context.Session,
                Slides = slideShapes,
                Controls = element.Controls,
                Indicators = element.Indicators,
                Interval = element.Interval,
                Keyboard = element.Keyboard,
                Pause = element.Pause,
                Wrap = element.Wrap
            };

            if (context.Updater != null) {
                if (context.Updater.TryUpdateModel(viewModel, context.Prefix, new[] { "Indices", "SlidesData" }, null)) {
                    var currentSlides = slides;
                    var newSlides = new List<Slide>(currentSlides.Count);

                    newSlides.AddRange(viewModel.Indices.Select(index => currentSlides[index]));
                    StoreSlides(element, newSlides);

                    element.Controls = viewModel.Controls;
                    element.Indicators = viewModel.Indicators;
                    element.Interval = viewModel.Interval;
                    element.Keyboard = viewModel.Keyboard;
                    element.Pause = viewModel.Pause;
                    element.Wrap = viewModel.Wrap;
                }
            }

            var slidesEditor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements/SlideShow.Slides", Prefix: context.Prefix, Model: viewModel);
            var propertiesEditor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements/SlideShow.Properties", Prefix: context.Prefix, Model: viewModel);

            slidesEditor.Metadata.Position = "Slides:0";
            propertiesEditor.Metadata.Position = "Properties:1";

            return Editor(context, slidesEditor, propertiesEditor);
        }

        protected override void OnDisplaying(SlideShow element, ElementDisplayingContext context) {
            var slideShapes = DisplaySlides(element, context).ToList();
            context.ElementShape.Slides = slideShapes;
        }

        private IEnumerable<dynamic> DisplaySlides(SlideShow element, ElementDisplayingContext context) {
            var slidesData = element.SlidesData;
            var slides = _slidesSerializer.Deserialize(slidesData).ToList();
            var slideShapes = slides.Select(x => _layoutManager.RenderLayout(x.LayoutData, content: context.Content));
            return slideShapes;
        }

        private IEnumerable<Slide> RetrieveSlides(SlideShow element) {
            var slidesData = element.SlidesData;
            var slides = _slidesSerializer.Deserialize(slidesData).ToList();

            return slides;
        }

        private void StoreSlides(SlideShow element, IEnumerable<Slide> slides) {
            element.SlidesData = _slidesSerializer.Serialize(slides);
        }
    }
}