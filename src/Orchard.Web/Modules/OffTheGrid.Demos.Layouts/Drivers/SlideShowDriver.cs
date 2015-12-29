using Orchard.Layouts.Framework.Drivers;
using OffTheGrid.Demos.Layouts.ViewModels;
using Orchard.Layouts.Framework.Display;
using System.Collections.Generic;
using OffTheGrid.Demos.Layouts.Models;
using System.Linq;
using Orchard.Layouts.Services;
using Orchard.ContentManagement;

namespace OffTheGrid.Demos.Layouts.Elements {
    public class SlideShowDriver : ElementDriver<SlideShow> {
        private ILayoutManager _layoutManager;
        private ISlidesSerializer _slidesSerializer;

        public SlideShowDriver(ILayoutManager layoutManager, ISlidesSerializer slidesSerializer) {
            _layoutManager = layoutManager;
            _slidesSerializer = slidesSerializer;
        }

        protected override EditorResult OnBuildEditor(SlideShow element, ElementEditorContext context) {
            var slides = RetrieveSlides(element).ToList();
            var slideShapes = DisplaySlides(slides, context.Content).ToList();
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
                if (context.Updater.TryUpdateModel(viewModel, context.Prefix, null, new[] { "Element", "Session", "Slides" })) {
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
            var playerEditor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements/SlideShow.Player", Prefix: context.Prefix, Model: viewModel);

            slidesEditor.Metadata.Position = "Slides:0";
            playerEditor.Metadata.Position = "Player:1";

            return Editor(context, slidesEditor, playerEditor);
        }

        protected override void OnDisplaying(SlideShow element, ElementDisplayingContext context) {
            var slides = RetrieveSlides(element);
            context.ElementShape.Slides = DisplaySlides(slides, context.Content).ToList();
        }

        private IEnumerable<dynamic> DisplaySlides(IEnumerable<Slide> slides, IContent content) {
            return slides.Select(x => _layoutManager.RenderLayout(x.LayoutData, content: content));
        }

        private IEnumerable<Slide> RetrieveSlides(SlideShow element) {
            var slidesData = element.SlidesData;
            return _slidesSerializer.Deserialize(slidesData);
        }

        private void StoreSlides(SlideShow element, IEnumerable<Slide> slides) {
            element.SlidesData = _slidesSerializer.Serialize(slides);
        }
    }
}