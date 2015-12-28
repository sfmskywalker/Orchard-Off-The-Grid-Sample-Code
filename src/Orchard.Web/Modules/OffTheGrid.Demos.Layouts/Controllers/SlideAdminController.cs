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
using System;
using System.Collections.Generic;
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
            return CreateOrUpdateSlide(viewModel, T("That slide has been added."));
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
            return CreateOrUpdateSlide(viewModel, T("That slide has been updated."));
        }

        [HttpPost]
        public ActionResult Delete(string session, int index) {
            UpdateSlideShowSlides(session, slides => {
                // Delete the slide at the specified index.
                slides.RemoveAt(index);
            });
            
            // Redirect back to the element editor. The ReturnUrl contains the session key.
            _notifier.Information(T("That slide has been deleted."));
            return RedirectToElementEditor(session);
        }

        /// <summary>
        /// Either adds or updates a slide, depending on the viewModel.SlideIndex value.
        /// If no index was specified, it means we're adding a slide.
        /// Otherwise, we're updating the slide at the specified index.
        /// </summary>
        private ActionResult CreateOrUpdateSlide(SlideEditorViewModel viewModel, LocalizedString successNotification) {
            UpdateSlideShowSlides(viewModel.Session, slides => {
                var slide = viewModel.SlideIndex != null ? slides[viewModel.SlideIndex.Value] : default(Slide);
                var slideLayout = _layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.Data, DescribeElementsContext.Empty);
                var recycleBin = (RecycleBin)_layoutModelMapper.ToLayoutModel(viewModel.LayoutEditor.RecycleBin, DescribeElementsContext.Empty).First();
                var context = new LayoutSavingContext {
                    Updater = this,
                    Elements = slideLayout,
                    RemovedElements = recycleBin.Elements
                };

                _elementManager.Saving(context);
                _elementManager.Removing(context);

                // Check if we are updating an existing slide or creating a new one.
                if (slide == null) {
                    slide = new Slide();
                    slides.Add(slide);
                }

                // Update the slide.
                slide.TemplateId = viewModel.LayoutEditor.TemplateId;
                slide.LayoutData = _layoutSerializer.Serialize(slideLayout);
            });
            
            // Redirect back to the element editor. The ReturnUrl contains the session key.
            _notifier.Information(successNotification);
            return RedirectToElementEditor(viewModel.Session);
        }

        /// <summary>
        /// Deserializes the SlideShow element from the object store,
        /// invokes the specified callback, passing in the list of slides of the slide show,
        /// which then is stored back into the slide show, which in turn is serialized again
        /// and stored in the object store.
        /// </summary>
        /// <param name="session">The key into the object store where the slide show is stored.</param>
        /// <param name="updater">The action to callback that adds / updates / removes from the specified list of slides.</param>
        private void UpdateSlideShowSlides(string session, Action<IList<Slide>> updater) {
            var slideShowSessionState = _objectStore.Get<ElementSessionState>(session);
            var elementData = ElementDataHelper.Deserialize(slideShowSessionState.ElementData);
            var slideShow = _elementManager.ActivateElement<SlideShow>(x => x.Data = elementData);
            var slides = _slidesSerializer.Deserialize(slideShow.SlidesData).ToList();

            // Manipulate the list of slides.
            updater(slides);

            // Serialize the in-memory list and assign it back to the SlidesData property of the SlideShow element.
            slideShow.SlidesData = _slidesSerializer.Serialize(slides);

            // Serialize the slide show element itself.
            slideShowSessionState.ElementData = ElementDataHelper.Serialize(slideShow.Data);

            // Replace the slide show in the object store with the updated data.
            _objectStore.Set(session, slideShowSessionState);
        }

        private ActionResult RedirectToElementEditor(string session) {
            return RedirectToAction("Edit", "Element", new { session = session, area = "Orchard.Layouts" });
        }
        
        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.Text);
        }
    }
}