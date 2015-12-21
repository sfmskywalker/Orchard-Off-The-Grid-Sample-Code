/*
** NOTE: This file is generated by Gulp and should not be edited directly!
** Any changes made directly to this file will be overwritten next time its asset group is processed by Gulp.
*/

angular
    .module("LayoutEditor")
    .directive("orcLayoutTile", ["scopeConfigurator", "environment",
        function (scopeConfigurator, environment) {
            return {
                restrict: "E",
                scope: { element: "=" },
                controller: ["$scope", "$element", "$attrs",
                    function ($scope, $element, $attrs) {
                        scopeConfigurator.configureForElement($scope, $element);
                        scopeConfigurator.configureForContainer($scope, $element);
                        $scope.sortableOptions["axis"] = "y";
                    }
                ],
                templateUrl: environment.templateUrl("Tile"),
                replace: true
            };
        }
    ]);
var LayoutEditor;
(function (LayoutEditor) {

    LayoutEditor.Tile = function (data, contentType, htmlId, htmlClass, htmlStyle, isTemplated, rule, hasEditor, backgroundUrl, children) {
        // Inherit from the Element base class.
        LayoutEditor.Element.call(this, "Tile", data, htmlId, htmlClass, htmlStyle, isTemplated, rule);

        // Inherit from the Container base class.
        LayoutEditor.Container.call(this, ["Canvas", "Grid", "Content"], children);

        // This Tile element is containable, which means it can be added to any container, including Tiles.
        this.isContainable = true;

        // Used by the layout editor to determine if it should launch
        // the element editor dialog when creating new Tile elements.
        // Also used by our "LayoutEditor.Template.Tile.cshtml" view that is used as the layout-tile directive's template.
        this.hasEditor = hasEditor;

        // The element type name, which is sent back to the element editor controller when being edited.
        this.contentType = contentType;

        // The "layout-common-holder" CSS class is used by the layout editor to identify drop targets.
        this.dropTargetClass = "layout-common-holder";

        // The configured background image URL.
        this.backgroundUrl = backgroundUrl;

        // Implements the toObject serialization function.
        this.toObject = function () {
            var result = this.elementToObject();
            result.children = this.childrenToObject();
            return result;
        };

        this.hasBackground = function () {
            return this.backgroundUrl && this.backgroundUrl.length > 0;
        };

        if (this.hasBackground()) {
            var styles = {
                "background-image": "url('" + this.backgroundUrl + "')",
                "background-size": "cover"
            };

            if (this.children.length == 0)
                this.templateStyles = styles;
            else {
                this.containerTemplateStyles = styles;
            }
        };
    };

    // Implements the factory function invoked by the element factory.
    LayoutEditor.Tile.from = function (value) {
        var result = new LayoutEditor.Tile(
            value.data,
            value.contentType,
            value.htmlId,
            value.htmlClass,
            value.htmlStyle,
            value.isTemplated,
            value.rule,
            value.hasEditor,
            value.backgroundUrl,
            LayoutEditor.childrenFrom(value.children));

        // Initializes the toolbox specific properties.
        result.toolboxIcon = value.toolboxIcon;
        result.toolboxLabel = value.toolboxLabel;
        result.toolboxDescription = value.toolboxDescription;

        return result;
    };

    // Registers the factory function with the element factory.
    LayoutEditor.registerFactory("Tile", function (value) {
        return LayoutEditor.Tile.from(value);
    });

})(LayoutEditor || (LayoutEditor = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIlRpbGVFbGVtZW50LmpzIiwiVGlsZS5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EsQUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQWxCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EiLCJmaWxlIjoiVGlsZUVsZW1lbnQuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGwsInZhciBMYXlvdXRFZGl0b3I7XHJcbihmdW5jdGlvbiAoTGF5b3V0RWRpdG9yKSB7XHJcblxyXG4gICAgTGF5b3V0RWRpdG9yLlRpbGUgPSBmdW5jdGlvbiAoZGF0YSwgY29udGVudFR5cGUsIGh0bWxJZCwgaHRtbENsYXNzLCBodG1sU3R5bGUsIGlzVGVtcGxhdGVkLCBydWxlLCBoYXNFZGl0b3IsIGJhY2tncm91bmRVcmwsIGNoaWxkcmVuKSB7XHJcbiAgICAgICAgLy8gSW5oZXJpdCBmcm9tIHRoZSBFbGVtZW50IGJhc2UgY2xhc3MuXHJcbiAgICAgICAgTGF5b3V0RWRpdG9yLkVsZW1lbnQuY2FsbCh0aGlzLCBcIlRpbGVcIiwgZGF0YSwgaHRtbElkLCBodG1sQ2xhc3MsIGh0bWxTdHlsZSwgaXNUZW1wbGF0ZWQsIHJ1bGUpO1xyXG5cclxuICAgICAgICAvLyBJbmhlcml0IGZyb20gdGhlIENvbnRhaW5lciBiYXNlIGNsYXNzLlxyXG4gICAgICAgIExheW91dEVkaXRvci5Db250YWluZXIuY2FsbCh0aGlzLCBbXCJDYW52YXNcIiwgXCJHcmlkXCIsIFwiQ29udGVudFwiXSwgY2hpbGRyZW4pO1xyXG5cclxuICAgICAgICAvLyBUaGlzIFRpbGUgZWxlbWVudCBpcyBjb250YWluYWJsZSwgd2hpY2ggbWVhbnMgaXQgY2FuIGJlIGFkZGVkIHRvIGFueSBjb250YWluZXIsIGluY2x1ZGluZyBUaWxlcy5cclxuICAgICAgICB0aGlzLmlzQ29udGFpbmFibGUgPSB0cnVlO1xyXG5cclxuICAgICAgICAvLyBVc2VkIGJ5IHRoZSBsYXlvdXQgZWRpdG9yIHRvIGRldGVybWluZSBpZiBpdCBzaG91bGQgbGF1bmNoXHJcbiAgICAgICAgLy8gdGhlIGVsZW1lbnQgZWRpdG9yIGRpYWxvZyB3aGVuIGNyZWF0aW5nIG5ldyBUaWxlIGVsZW1lbnRzLlxyXG4gICAgICAgIC8vIEFsc28gdXNlZCBieSBvdXIgXCJMYXlvdXRFZGl0b3IuVGVtcGxhdGUuVGlsZS5jc2h0bWxcIiB2aWV3IHRoYXQgaXMgdXNlZCBhcyB0aGUgbGF5b3V0LXRpbGUgZGlyZWN0aXZlJ3MgdGVtcGxhdGUuXHJcbiAgICAgICAgdGhpcy5oYXNFZGl0b3IgPSBoYXNFZGl0b3I7XHJcblxyXG4gICAgICAgIC8vIFRoZSBlbGVtZW50IHR5cGUgbmFtZSwgd2hpY2ggaXMgc2VudCBiYWNrIHRvIHRoZSBlbGVtZW50IGVkaXRvciBjb250cm9sbGVyIHdoZW4gYmVpbmcgZWRpdGVkLlxyXG4gICAgICAgIHRoaXMuY29udGVudFR5cGUgPSBjb250ZW50VHlwZTtcclxuXHJcbiAgICAgICAgLy8gVGhlIFwibGF5b3V0LWNvbW1vbi1ob2xkZXJcIiBDU1MgY2xhc3MgaXMgdXNlZCBieSB0aGUgbGF5b3V0IGVkaXRvciB0byBpZGVudGlmeSBkcm9wIHRhcmdldHMuXHJcbiAgICAgICAgdGhpcy5kcm9wVGFyZ2V0Q2xhc3MgPSBcImxheW91dC1jb21tb24taG9sZGVyXCI7XHJcblxyXG4gICAgICAgIC8vIFRoZSBjb25maWd1cmVkIGJhY2tncm91bmQgaW1hZ2UgVVJMLlxyXG4gICAgICAgIHRoaXMuYmFja2dyb3VuZFVybCA9IGJhY2tncm91bmRVcmw7XHJcblxyXG4gICAgICAgIC8vIEltcGxlbWVudHMgdGhlIHRvT2JqZWN0IHNlcmlhbGl6YXRpb24gZnVuY3Rpb24uXHJcbiAgICAgICAgdGhpcy50b09iamVjdCA9IGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgdmFyIHJlc3VsdCA9IHRoaXMuZWxlbWVudFRvT2JqZWN0KCk7XHJcbiAgICAgICAgICAgIHJlc3VsdC5jaGlsZHJlbiA9IHRoaXMuY2hpbGRyZW5Ub09iamVjdCgpO1xyXG4gICAgICAgICAgICByZXR1cm4gcmVzdWx0O1xyXG4gICAgICAgIH07XHJcblxyXG4gICAgICAgIHRoaXMuaGFzQmFja2dyb3VuZCA9IGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgcmV0dXJuIHRoaXMuYmFja2dyb3VuZFVybCAmJiB0aGlzLmJhY2tncm91bmRVcmwubGVuZ3RoID4gMDtcclxuICAgICAgICB9O1xyXG5cclxuICAgICAgICBpZiAodGhpcy5oYXNCYWNrZ3JvdW5kKCkpIHtcclxuICAgICAgICAgICAgdmFyIHN0eWxlcyA9IHtcclxuICAgICAgICAgICAgICAgIFwiYmFja2dyb3VuZC1pbWFnZVwiOiBcInVybCgnXCIgKyB0aGlzLmJhY2tncm91bmRVcmwgKyBcIicpXCIsXHJcbiAgICAgICAgICAgICAgICBcImJhY2tncm91bmQtc2l6ZVwiOiBcImNvdmVyXCJcclxuICAgICAgICAgICAgfTtcclxuXHJcbiAgICAgICAgICAgIGlmICh0aGlzLmNoaWxkcmVuLmxlbmd0aCA9PSAwKVxyXG4gICAgICAgICAgICAgICAgdGhpcy50ZW1wbGF0ZVN0eWxlcyA9IHN0eWxlcztcclxuICAgICAgICAgICAgZWxzZSB7XHJcbiAgICAgICAgICAgICAgICB0aGlzLmNvbnRhaW5lclRlbXBsYXRlU3R5bGVzID0gc3R5bGVzO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfTtcclxuICAgIH07XHJcblxyXG4gICAgLy8gSW1wbGVtZW50cyB0aGUgZmFjdG9yeSBmdW5jdGlvbiBpbnZva2VkIGJ5IHRoZSBlbGVtZW50IGZhY3RvcnkuXHJcbiAgICBMYXlvdXRFZGl0b3IuVGlsZS5mcm9tID0gZnVuY3Rpb24gKHZhbHVlKSB7XHJcbiAgICAgICAgdmFyIHJlc3VsdCA9IG5ldyBMYXlvdXRFZGl0b3IuVGlsZShcclxuICAgICAgICAgICAgdmFsdWUuZGF0YSxcclxuICAgICAgICAgICAgdmFsdWUuY29udGVudFR5cGUsXHJcbiAgICAgICAgICAgIHZhbHVlLmh0bWxJZCxcclxuICAgICAgICAgICAgdmFsdWUuaHRtbENsYXNzLFxyXG4gICAgICAgICAgICB2YWx1ZS5odG1sU3R5bGUsXHJcbiAgICAgICAgICAgIHZhbHVlLmlzVGVtcGxhdGVkLFxyXG4gICAgICAgICAgICB2YWx1ZS5ydWxlLFxyXG4gICAgICAgICAgICB2YWx1ZS5oYXNFZGl0b3IsXHJcbiAgICAgICAgICAgIHZhbHVlLmJhY2tncm91bmRVcmwsXHJcbiAgICAgICAgICAgIExheW91dEVkaXRvci5jaGlsZHJlbkZyb20odmFsdWUuY2hpbGRyZW4pKTtcclxuXHJcbiAgICAgICAgLy8gSW5pdGlhbGl6ZXMgdGhlIHRvb2xib3ggc3BlY2lmaWMgcHJvcGVydGllcy5cclxuICAgICAgICByZXN1bHQudG9vbGJveEljb24gPSB2YWx1ZS50b29sYm94SWNvbjtcclxuICAgICAgICByZXN1bHQudG9vbGJveExhYmVsID0gdmFsdWUudG9vbGJveExhYmVsO1xyXG4gICAgICAgIHJlc3VsdC50b29sYm94RGVzY3JpcHRpb24gPSB2YWx1ZS50b29sYm94RGVzY3JpcHRpb247XHJcblxyXG4gICAgICAgIHJldHVybiByZXN1bHQ7XHJcbiAgICB9O1xyXG5cclxuICAgIC8vIFJlZ2lzdGVycyB0aGUgZmFjdG9yeSBmdW5jdGlvbiB3aXRoIHRoZSBlbGVtZW50IGZhY3RvcnkuXHJcbiAgICBMYXlvdXRFZGl0b3IucmVnaXN0ZXJGYWN0b3J5KFwiVGlsZVwiLCBmdW5jdGlvbiAodmFsdWUpIHtcclxuICAgICAgICByZXR1cm4gTGF5b3V0RWRpdG9yLlRpbGUuZnJvbSh2YWx1ZSk7XHJcbiAgICB9KTtcclxuXHJcbn0pKExheW91dEVkaXRvciB8fCAoTGF5b3V0RWRpdG9yID0ge30pKTtcclxuIl0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
