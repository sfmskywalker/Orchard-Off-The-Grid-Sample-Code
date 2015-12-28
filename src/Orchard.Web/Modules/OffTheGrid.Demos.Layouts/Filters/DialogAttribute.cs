using System;
using System.Web.Mvc;
using Orchard;

namespace OffTheGrid.Demos.Layouts.Filters {
    public class DialogAttribute : ActionFilterAttribute {
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            var workContext = filterContext.GetWorkContext();
            workContext.Layout.Metadata.Alternates.Add("Layout__Dialog");
        }
    }
}