using System;
using Orchard.ContentManagement;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace OffTheGrid.Demos.Layouts.Helpers {
    public static class ElementExtensions {
        private const string FadeInKey = "FadeIn";
        private const string DefaultFadeInValue = "false";

        public static bool GetFadeIn(this Element element) {
            return XmlHelper.Parse<bool>(element.Data.Get(FadeInKey, DefaultFadeInValue));
        }

        public static void SetFadeIn(this Element element, bool value) {
            element.Data[FadeInKey] = XmlHelper.ToString(value);
        }
    }
}