using System.Web.Mvc;
using OffTheGrid.Demos.Layouts.Models;
using OffTheGrid.Demos.Layouts.ViewModels;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Services;
using Orchard.UI.Admin;
using Orchard.Layouts.Helpers;
using Orchard.ContentManagement;
using Orchard.Localization;
using System.Linq;
using Orchard.UI.Notify;
using Orchard.Layouts.Models;
using OffTheGrid.Demos.Layouts.Elements;
using OffTheGrid.Demos.Layouts.Filters;
using Orchard.Layouts.Elements;

namespace OffTheGrid.Demos.Layouts.Controllers {
    [Admin]
    [Dialog]
    public class SlideAdminController : Controller, IUpdateModel {
        private readonly ILayoutEditorFactory _layoutEditorFactory;
        private readonly ILayoutSerializer _layoutSerializer;
        private readonly ILayoutModelMapper _layoutModelMapper;
        private readonly IElementManager _elementManager;
        private readonly IObjectStore _objectStore;
        private readonly INotifier _notifier;
        private readonly ISlidesSerializer _slidesSerializer;

        public SlideAdminController(
            ILayoutEditorFactory layoutEditorFactory, 
            ILayoutSerializer layoutSerializer,
            ILayoutModelMapper layoutModelMapper,
            IElementManager elementManager,
            IObjectStore objectStore,
            ISlidesSerializer slidesSerializer,
            INotifier notifier) {

            _layoutEditorFactory = layoutEditorFactory;
            _layoutSerializer = layoutSerializer;
            _layoutModelMapper = layoutModelMapper;
            _elementManager = elementManager;
            _objectStore = objectStore;
            _slidesSerializer = slidesSerializer;
            _notifier = notifier;
        }

        public Localizer T { get; set; }

        public ActionResult Create(string session) {
            var viewModel = new SlideEditorViewModel {
                Session = session,
                LayoutEditor = _layoutEditorFactory.Create(null, _objectStore.GenerateKey())
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SlideEditorViewModel viewModel) {
            var slideShowSessionState = _objectStore.Get<ElementSessionState>(viewModel.Session);
            var elementData = ElementDataHelper.Deserialize(slideShowSessionState.ElementData);
            var slideShow = _elementManager.ActivateElement<SlideShow>(x => x.Data = elementData);
            var slides = _slidesSerializer.Deserialize(slideShow.SlidesData).ToList();
            var slideLayout = _layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.Data, DescribeElementsContext.Empty);
            var recycleBin = (RecycleBin)_layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.RecycleBin, DescribeElementsContext.Empty).SingleOrDefault();
            var removedElements = recycleBin != null ? recycleBin.Elements : Enumerable.Empty<Element>();
            var context = new LayoutSavingContext {
                Updater = this,
                Elements = slideLayout,
                RemovedElements = removedElements
            };

            _elementManager.Saving(context);
            _elementManager.Removing(context);

            // Add the new slide to the in-memory slides list.
            slides.Add(new Slide { LayoutData = _layoutSerializer.Serialize(slideLayout) });

            // Serialize the in-memory list and assign it back to the SlidesData property of the SlideShow element.
            slideShow.SlidesData = _slidesSerializer.Serialize(slides);

            // Serialize the slide show element itself.
            slideShowSessionState.ElementData = ElementDataHelper.Serialize(slideShow.Data);

            // Replace the slide show in the object store with the updated data.
            _objectStore.Set(viewModel.Session, slideShowSessionState);

            // Redirect back to the element editor. The ReturnUrl contains the session key.
            _notifier.Information(T("That slide has been added."));
            return RedirectToAction("Edit", "Element", new { session = viewModel.Session, area = "Orchard.Layouts" });
        }

        public ActionResult Edit(string session, int index) {
            var slideShowSessionState = _objectStore.Get<ElementSessionState>(session);
            var elementData = ElementDataHelper.Deserialize(slideShowSessionState.ElementData);
            var slideShow = _elementManager.ActivateElement<SlideShow>(x => x.Data = elementData);
            var slides = _slidesSerializer.Deserialize(slideShow.SlidesData).ToList();
            var slide = slides[index];
            var viewModel = new SlideEditorViewModel {
                SlideIndex = index,
                Session = session,
                LayoutEditor = _layoutEditorFactory.Create(slide.LayoutData, _objectStore.GenerateKey(), slide.TemplateId)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SlideEditorViewModel viewModel, int index) {
            var slideShowSessionState = _objectStore.Get<ElementSessionState>(viewModel.Session);
            var elementData = ElementDataHelper.Deserialize(slideShowSessionState.ElementData);
            var slideShow = _elementManager.ActivateElement<SlideShow>(x => x.Data = elementData);
            var slides = _slidesSerializer.Deserialize(slideShow.SlidesData).ToList();
            var slide = slides[viewModel.SlideIndex];
            var slideLayout = _layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.Data, DescribeElementsContext.Empty);
            var removedElements = _layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.RecycleBin, DescribeElementsContext.Empty);
            var context = new LayoutSavingContext {
                Updater = this,
                Elements = slideLayout,
                RemovedElements = removedElements
            };

            _elementManager.Saving(context);
            _elementManager.Removing(context);

            // Update the slide.
            slide.TemplateId = viewModel.LayoutEditor.TemplateId;
            slide.LayoutData = _layoutSerializer.Serialize(slideLayout);

            // Serialize the in-memory list and assign it back to the SlidesData property of the SlideShow element.
            slideShow.SlidesData = _slidesSerializer.Serialize(slides);

            // Serialize the slide show element itself.
            slideShowSessionState.ElementData = ElementDataHelper.Serialize(slideShow.Data);

            // Replace the slide show in the object store with the updated data.
            _objectStore.Set(viewModel.Session, slideShowSessionState);

            // Redirect back to the element editor. The ReturnUrl contains the session key.
            _notifier.Information(T("That slide has been updated."));
            return RedirectToAction("Edit", "Element", new { session = viewModel.Session, area = "Orchard.Layouts" });
        }
        
        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.Text);
        }
    }
}