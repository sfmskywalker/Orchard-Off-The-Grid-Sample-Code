using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Services;
using Orchard.Layouts.ViewModels;
using Orchard.UI.Admin;

namespace OffTheGrid.Demos.Layouts.Controllers {
    [Admin] // The layout editor is designed to work from the back end.
    public class ElementsApiController : Controller {
        private readonly IElementManager _elementManager;
        private readonly IElementDisplay _elementDisplay;
        private readonly ILayoutSerializer _layoutSerializer;
        private readonly ILayoutEditorFactory _layoutEditorFactory;
        private readonly ILayoutModelMapper _modelMapper;

        public ElementsApiController(
            IElementManager elementManager,
            IElementDisplay elementDisplay,
            ILayoutSerializer layoutSerializer,
            ILayoutEditorFactory layoutEditorFactory,
            ILayoutModelMapper modelMapper) {

            _elementManager = elementManager;
            _elementDisplay = elementDisplay;
            _layoutSerializer = layoutSerializer;
            _layoutEditorFactory = layoutEditorFactory;
            _modelMapper = modelMapper;
        }

        [ValidateInput(false)] // The submitted data may contain HTML.
        public ActionResult Index(LayoutEditor layoutEditor /* The LayoutEditor type serves as a viewmodel which can be modelbound. */) {

            IEnumerable<Element> layout;
            string layoutData = null;

            if (layoutEditor.Data != null) {
                // The posted layout data is not the raw Layouts JSON format, but a more tailored one specific to the layout editor.
                // Before we can use it, we need to map it to the standard layout format.
                layout = _modelMapper.ToLayoutModel(layoutEditor.Data, DescribeElementsContext.Empty).ToList();

                // Serialize the layout.
                layoutData = _layoutSerializer.Serialize(layout);
            }
            else {
                // Create a default hierarchy of elements.
                layout = CreateDefaultLayout();

                // Serialize the layout.
                layoutData = _layoutSerializer.Serialize(layout);
            }

            // The session key is used for the IObjectStore service
            // used by the layout editor to transfer data to the element editor.
            // The actual value doesn't matter, just as long as its unique within the application.
            var sessionKey = "DemoSessionKey";

            // Create and initialize a new LayoutEditor object.
            layoutEditor = _layoutEditorFactory.Create(layoutData, sessionKey);

            // Assign the LayoutEditor to a property on the dynamic ViewBag.
            ViewBag.LayoutEditor = layoutEditor;

            return View();
        }

        // Creates an element tree with a default layout (Grid, Row, and two Columns).
        private IEnumerable<Element> CreateDefaultLayout() {
            return new[] { New<Canvas>(canvas => {
                canvas.Elements.Add(
                    New<Grid>(grid => {
                    // Row.
                    grid.Elements.Add(New<Row>(row => {
                        // Column 1.
                        row.Elements.Add(New<Column>(column => {
                            column.Width = 6;
                            column.Elements.Add(New<Html>(html => html.Content = "This is the <strong>first</strong> column."));
                        }));

                        // Column 2.
                        row.Elements.Add(New<Column>(column => {
                            column.Width = 6;
                            column.Elements.Add(New<Html>(html => html.Content = "This is the <strong>second</strong> column."));
                        }));
                    }));
                }));
            })};
        }

        // An alias to IElementManager.ActivateElement<T>.
        private T New<T>(Action<T> initialize) where T : Element {
            return _elementManager.ActivateElement<T>(initialize);
        }
    }
}