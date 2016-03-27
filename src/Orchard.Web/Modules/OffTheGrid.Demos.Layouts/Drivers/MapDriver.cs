using System.Collections.Generic;
using OffTheGrid.Demos.Layouts.Elements;
using Orchard.Forms.Services;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Services;
using Orchard.Services;

namespace OffTheGrid.Demos.Layouts.Drivers {
    public class MapDriver : FormsElementDriver<Map> {
        public MapDriver(IFormsBasedElementServices formsServices) : base(formsServices) { }

        protected override IEnumerable<string> FormNames {
            get { yield return "MapEditor"; }
        }

        protected override void DescribeForm(DescribeContext context) {
            context.Form("MapEditor", shapeFactory => {
                var shape = (dynamic)shapeFactory;
                var form = shape.Fieldset(
                    Id: "Map",
                    _Latitude: shape.Textbox(
                        Id: "Latitude",
                        Name: "Latitude", // -> This name needs to match the name of the Latitude property of the Map class.
                        Title: T("Latitude"),
                        Classes: new[] { "text", "medium" },
                        Description: T("The latitude of the location to show on the map.")),
                    _Longitude: shape.Textbox(
                        Id: "Longitude",
                        Name: "Longitude", // -> This name needs to match the name of the Longitude property of the Map class.
                        Title: T("Longitude"),
                        Classes: new[] { "text", "medium" },
                        Description: T("The longitude of the location to show on the map.")));

                return form;
            });
        }
    }

    //public class MapDriver : ElementDriver<Map> {

    //    protected override EditorResult OnBuildEditor(Map element, ElementEditorContext context) {

    //        // If an Updater is specified, it means the element editor form is being submitted
    //        // and we need to store the submitted data.
    //        context.Updater?.TryUpdateModel(element, context.Prefix, null, null);

    //        // Create the EditorTemplate shape.
    //        var editor = context.ShapeFactory.EditorTemplate(
    //            TemplateName: "Elements/Map",
    //            Model: element,
    //            Prefix: context.Prefix);

    //        return Editor(context, editor);
    //    }
    //}
}