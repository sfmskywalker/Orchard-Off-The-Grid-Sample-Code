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

    LayoutEditor.Tile = function (data, contentType, htmlId, htmlClass, htmlStyle, isTemplated, rule, hasEditor, backgroundUrl, backgroundSize, children) {
        var self = this;

        // Inherit from the Element base class.
        LayoutEditor.Element.call(self, "Tile", data, htmlId, htmlClass, htmlStyle, isTemplated, rule);

        // Inherit from the Container base class.
        LayoutEditor.Container.call(self, ["Canvas", "Grid", "Content"], children);

        // This Tile element is containable, which means it can be added to any container, including Tiles.
        self.isContainable = true;

        // Used by the layout editor to determine if it should launch
        // the element editor dialog when creating new Tile elements.
        // Also used by our "LayoutEditor.Template.Tile.cshtml" view that is used as the layout-tile directive's template.
        self.hasEditor = hasEditor;

        // The element type name, which is sent back to the element editor controller when being edited.
        self.contentType = contentType;

        // The "layout-common-holder" CSS class is used by the layout editor to identify drop targets.
        self.dropTargetClass = "layout-common-holder";

        // The configured background image URL and background size.
        self.backgroundUrl = backgroundUrl;
        self.backgroundSize = backgroundSize;

        // Implements the toObject serialization function.
        // This is called when the layout is being serialized into JSON.
        var toObject = self.toObject; // Get a reference to the default implementation before we override it.
        self.toObject = function () {
            var result = toObject(); // Invoke the original (base) implementation.
            result.children = self.childrenToObject();
            result.backgroundUrl = self.backgroundUrl;
            result.backgroundSize = self.backgroundSize;
            return result;
        };

        // Override the getEditorObject so we can include our backgroundSize property.
        // This is called when the element editor dialog is being invoked and we need to
        // pass in the client side values.
        var getEditorObjectBase = this.getEditorObject;
        this.getEditorObject = function () {
            var props = getEditorObjectBase();
            props.BackgroundSize = self.backgroundSize;
            return props;
        }

        // Executed after the element editordialog closes.
        this.applyElementEditorModel = function (data) {
            self.backgroundUrl = data.backgroundUrl;
            self.backgroundSize = data.backgroundSize;
            self.applyBackground();
        }

        this.hasBackground = function () {
            return self.backgroundUrl && self.backgroundUrl.length > 0;
        };

        this.applyBackground = function () {
            if (self.hasBackground()) {
                var styles = {
                    "background-image": "url('" + self.backgroundUrl + "')",
                    "background-size": self.backgroundSize && self.backgroundSize.length > 0 ? self.backgroundSize : "cover"
                };

                if (self.children.length == 0)
                    self.templateStyles = styles;
                else
                    self.containerTemplateStyles = styles;
            }
            else {
                self.templateStyles = {};
                self.containerTemplateStyles = {};
            }
        }

        self.applyBackground();
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
            value.backgroundSize,
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

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIlRpbGVFbGVtZW50LmpzIiwiVGlsZS5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EsQUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQWxCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBIiwiZmlsZSI6IlRpbGVFbGVtZW50LmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsLCJ2YXIgTGF5b3V0RWRpdG9yO1xyXG4oZnVuY3Rpb24gKExheW91dEVkaXRvcikge1xyXG5cclxuICAgIExheW91dEVkaXRvci5UaWxlID0gZnVuY3Rpb24gKGRhdGEsIGNvbnRlbnRUeXBlLCBodG1sSWQsIGh0bWxDbGFzcywgaHRtbFN0eWxlLCBpc1RlbXBsYXRlZCwgcnVsZSwgaGFzRWRpdG9yLCBiYWNrZ3JvdW5kVXJsLCBiYWNrZ3JvdW5kU2l6ZSwgY2hpbGRyZW4pIHtcclxuICAgICAgICB2YXIgc2VsZiA9IHRoaXM7XHJcblxyXG4gICAgICAgIC8vIEluaGVyaXQgZnJvbSB0aGUgRWxlbWVudCBiYXNlIGNsYXNzLlxyXG4gICAgICAgIExheW91dEVkaXRvci5FbGVtZW50LmNhbGwoc2VsZiwgXCJUaWxlXCIsIGRhdGEsIGh0bWxJZCwgaHRtbENsYXNzLCBodG1sU3R5bGUsIGlzVGVtcGxhdGVkLCBydWxlKTtcclxuXHJcbiAgICAgICAgLy8gSW5oZXJpdCBmcm9tIHRoZSBDb250YWluZXIgYmFzZSBjbGFzcy5cclxuICAgICAgICBMYXlvdXRFZGl0b3IuQ29udGFpbmVyLmNhbGwoc2VsZiwgW1wiQ2FudmFzXCIsIFwiR3JpZFwiLCBcIkNvbnRlbnRcIl0sIGNoaWxkcmVuKTtcclxuXHJcbiAgICAgICAgLy8gVGhpcyBUaWxlIGVsZW1lbnQgaXMgY29udGFpbmFibGUsIHdoaWNoIG1lYW5zIGl0IGNhbiBiZSBhZGRlZCB0byBhbnkgY29udGFpbmVyLCBpbmNsdWRpbmcgVGlsZXMuXHJcbiAgICAgICAgc2VsZi5pc0NvbnRhaW5hYmxlID0gdHJ1ZTtcclxuXHJcbiAgICAgICAgLy8gVXNlZCBieSB0aGUgbGF5b3V0IGVkaXRvciB0byBkZXRlcm1pbmUgaWYgaXQgc2hvdWxkIGxhdW5jaFxyXG4gICAgICAgIC8vIHRoZSBlbGVtZW50IGVkaXRvciBkaWFsb2cgd2hlbiBjcmVhdGluZyBuZXcgVGlsZSBlbGVtZW50cy5cclxuICAgICAgICAvLyBBbHNvIHVzZWQgYnkgb3VyIFwiTGF5b3V0RWRpdG9yLlRlbXBsYXRlLlRpbGUuY3NodG1sXCIgdmlldyB0aGF0IGlzIHVzZWQgYXMgdGhlIGxheW91dC10aWxlIGRpcmVjdGl2ZSdzIHRlbXBsYXRlLlxyXG4gICAgICAgIHNlbGYuaGFzRWRpdG9yID0gaGFzRWRpdG9yO1xyXG5cclxuICAgICAgICAvLyBUaGUgZWxlbWVudCB0eXBlIG5hbWUsIHdoaWNoIGlzIHNlbnQgYmFjayB0byB0aGUgZWxlbWVudCBlZGl0b3IgY29udHJvbGxlciB3aGVuIGJlaW5nIGVkaXRlZC5cclxuICAgICAgICBzZWxmLmNvbnRlbnRUeXBlID0gY29udGVudFR5cGU7XHJcblxyXG4gICAgICAgIC8vIFRoZSBcImxheW91dC1jb21tb24taG9sZGVyXCIgQ1NTIGNsYXNzIGlzIHVzZWQgYnkgdGhlIGxheW91dCBlZGl0b3IgdG8gaWRlbnRpZnkgZHJvcCB0YXJnZXRzLlxyXG4gICAgICAgIHNlbGYuZHJvcFRhcmdldENsYXNzID0gXCJsYXlvdXQtY29tbW9uLWhvbGRlclwiO1xyXG5cclxuICAgICAgICAvLyBUaGUgY29uZmlndXJlZCBiYWNrZ3JvdW5kIGltYWdlIFVSTCBhbmQgYmFja2dyb3VuZCBzaXplLlxyXG4gICAgICAgIHNlbGYuYmFja2dyb3VuZFVybCA9IGJhY2tncm91bmRVcmw7XHJcbiAgICAgICAgc2VsZi5iYWNrZ3JvdW5kU2l6ZSA9IGJhY2tncm91bmRTaXplO1xyXG5cclxuICAgICAgICAvLyBJbXBsZW1lbnRzIHRoZSB0b09iamVjdCBzZXJpYWxpemF0aW9uIGZ1bmN0aW9uLlxyXG4gICAgICAgIC8vIFRoaXMgaXMgY2FsbGVkIHdoZW4gdGhlIGxheW91dCBpcyBiZWluZyBzZXJpYWxpemVkIGludG8gSlNPTi5cclxuICAgICAgICB2YXIgdG9PYmplY3QgPSBzZWxmLnRvT2JqZWN0OyAvLyBHZXQgYSByZWZlcmVuY2UgdG8gdGhlIGRlZmF1bHQgaW1wbGVtZW50YXRpb24gYmVmb3JlIHdlIG92ZXJyaWRlIGl0LlxyXG4gICAgICAgIHNlbGYudG9PYmplY3QgPSBmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgIHZhciByZXN1bHQgPSB0b09iamVjdCgpOyAvLyBJbnZva2UgdGhlIG9yaWdpbmFsIChiYXNlKSBpbXBsZW1lbnRhdGlvbi5cclxuICAgICAgICAgICAgcmVzdWx0LmNoaWxkcmVuID0gc2VsZi5jaGlsZHJlblRvT2JqZWN0KCk7XHJcbiAgICAgICAgICAgIHJlc3VsdC5iYWNrZ3JvdW5kVXJsID0gc2VsZi5iYWNrZ3JvdW5kVXJsO1xyXG4gICAgICAgICAgICByZXN1bHQuYmFja2dyb3VuZFNpemUgPSBzZWxmLmJhY2tncm91bmRTaXplO1xyXG4gICAgICAgICAgICByZXR1cm4gcmVzdWx0O1xyXG4gICAgICAgIH07XHJcblxyXG4gICAgICAgIC8vIE92ZXJyaWRlIHRoZSBnZXRFZGl0b3JPYmplY3Qgc28gd2UgY2FuIGluY2x1ZGUgb3VyIGJhY2tncm91bmRTaXplIHByb3BlcnR5LlxyXG4gICAgICAgIC8vIFRoaXMgaXMgY2FsbGVkIHdoZW4gdGhlIGVsZW1lbnQgZWRpdG9yIGRpYWxvZyBpcyBiZWluZyBpbnZva2VkIGFuZCB3ZSBuZWVkIHRvXHJcbiAgICAgICAgLy8gcGFzcyBpbiB0aGUgY2xpZW50IHNpZGUgdmFsdWVzLlxyXG4gICAgICAgIHZhciBnZXRFZGl0b3JPYmplY3RCYXNlID0gdGhpcy5nZXRFZGl0b3JPYmplY3Q7XHJcbiAgICAgICAgdGhpcy5nZXRFZGl0b3JPYmplY3QgPSBmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgIHZhciBwcm9wcyA9IGdldEVkaXRvck9iamVjdEJhc2UoKTtcclxuICAgICAgICAgICAgcHJvcHMuQmFja2dyb3VuZFNpemUgPSBzZWxmLmJhY2tncm91bmRTaXplO1xyXG4gICAgICAgICAgICByZXR1cm4gcHJvcHM7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICAvLyBFeGVjdXRlZCBhZnRlciB0aGUgZWxlbWVudCBlZGl0b3JkaWFsb2cgY2xvc2VzLlxyXG4gICAgICAgIHRoaXMuYXBwbHlFbGVtZW50RWRpdG9yTW9kZWwgPSBmdW5jdGlvbiAoZGF0YSkge1xyXG4gICAgICAgICAgICBzZWxmLmJhY2tncm91bmRVcmwgPSBkYXRhLmJhY2tncm91bmRVcmw7XHJcbiAgICAgICAgICAgIHNlbGYuYmFja2dyb3VuZFNpemUgPSBkYXRhLmJhY2tncm91bmRTaXplO1xyXG4gICAgICAgICAgICBzZWxmLmFwcGx5QmFja2dyb3VuZCgpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgdGhpcy5oYXNCYWNrZ3JvdW5kID0gZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICByZXR1cm4gc2VsZi5iYWNrZ3JvdW5kVXJsICYmIHNlbGYuYmFja2dyb3VuZFVybC5sZW5ndGggPiAwO1xyXG4gICAgICAgIH07XHJcblxyXG4gICAgICAgIHRoaXMuYXBwbHlCYWNrZ3JvdW5kID0gZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICBpZiAoc2VsZi5oYXNCYWNrZ3JvdW5kKCkpIHtcclxuICAgICAgICAgICAgICAgIHZhciBzdHlsZXMgPSB7XHJcbiAgICAgICAgICAgICAgICAgICAgXCJiYWNrZ3JvdW5kLWltYWdlXCI6IFwidXJsKCdcIiArIHNlbGYuYmFja2dyb3VuZFVybCArIFwiJylcIixcclxuICAgICAgICAgICAgICAgICAgICBcImJhY2tncm91bmQtc2l6ZVwiOiBzZWxmLmJhY2tncm91bmRTaXplICYmIHNlbGYuYmFja2dyb3VuZFNpemUubGVuZ3RoID4gMCA/IHNlbGYuYmFja2dyb3VuZFNpemUgOiBcImNvdmVyXCJcclxuICAgICAgICAgICAgICAgIH07XHJcblxyXG4gICAgICAgICAgICAgICAgaWYgKHNlbGYuY2hpbGRyZW4ubGVuZ3RoID09IDApXHJcbiAgICAgICAgICAgICAgICAgICAgc2VsZi50ZW1wbGF0ZVN0eWxlcyA9IHN0eWxlcztcclxuICAgICAgICAgICAgICAgIGVsc2VcclxuICAgICAgICAgICAgICAgICAgICBzZWxmLmNvbnRhaW5lclRlbXBsYXRlU3R5bGVzID0gc3R5bGVzO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIGVsc2Uge1xyXG4gICAgICAgICAgICAgICAgc2VsZi50ZW1wbGF0ZVN0eWxlcyA9IHt9O1xyXG4gICAgICAgICAgICAgICAgc2VsZi5jb250YWluZXJUZW1wbGF0ZVN0eWxlcyA9IHt9O1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBzZWxmLmFwcGx5QmFja2dyb3VuZCgpO1xyXG4gICAgfTtcclxuXHJcbiAgICAvLyBJbXBsZW1lbnRzIHRoZSBmYWN0b3J5IGZ1bmN0aW9uIGludm9rZWQgYnkgdGhlIGVsZW1lbnQgZmFjdG9yeS5cclxuICAgIExheW91dEVkaXRvci5UaWxlLmZyb20gPSBmdW5jdGlvbiAodmFsdWUpIHtcclxuICAgICAgICB2YXIgcmVzdWx0ID0gbmV3IExheW91dEVkaXRvci5UaWxlKFxyXG4gICAgICAgICAgICB2YWx1ZS5kYXRhLFxyXG4gICAgICAgICAgICB2YWx1ZS5jb250ZW50VHlwZSxcclxuICAgICAgICAgICAgdmFsdWUuaHRtbElkLFxyXG4gICAgICAgICAgICB2YWx1ZS5odG1sQ2xhc3MsXHJcbiAgICAgICAgICAgIHZhbHVlLmh0bWxTdHlsZSxcclxuICAgICAgICAgICAgdmFsdWUuaXNUZW1wbGF0ZWQsXHJcbiAgICAgICAgICAgIHZhbHVlLnJ1bGUsXHJcbiAgICAgICAgICAgIHZhbHVlLmhhc0VkaXRvcixcclxuICAgICAgICAgICAgdmFsdWUuYmFja2dyb3VuZFVybCxcclxuICAgICAgICAgICAgdmFsdWUuYmFja2dyb3VuZFNpemUsXHJcbiAgICAgICAgICAgIExheW91dEVkaXRvci5jaGlsZHJlbkZyb20odmFsdWUuY2hpbGRyZW4pKTtcclxuXHJcbiAgICAgICAgLy8gSW5pdGlhbGl6ZXMgdGhlIHRvb2xib3ggc3BlY2lmaWMgcHJvcGVydGllcy5cclxuICAgICAgICByZXN1bHQudG9vbGJveEljb24gPSB2YWx1ZS50b29sYm94SWNvbjtcclxuICAgICAgICByZXN1bHQudG9vbGJveExhYmVsID0gdmFsdWUudG9vbGJveExhYmVsO1xyXG4gICAgICAgIHJlc3VsdC50b29sYm94RGVzY3JpcHRpb24gPSB2YWx1ZS50b29sYm94RGVzY3JpcHRpb247XHJcblxyXG4gICAgICAgIHJldHVybiByZXN1bHQ7XHJcbiAgICB9O1xyXG5cclxuICAgIC8vIFJlZ2lzdGVycyB0aGUgZmFjdG9yeSBmdW5jdGlvbiB3aXRoIHRoZSBlbGVtZW50IGZhY3RvcnkuXHJcbiAgICBMYXlvdXRFZGl0b3IucmVnaXN0ZXJGYWN0b3J5KFwiVGlsZVwiLCBmdW5jdGlvbiAodmFsdWUpIHtcclxuICAgICAgICByZXR1cm4gTGF5b3V0RWRpdG9yLlRpbGUuZnJvbSh2YWx1ZSk7XHJcbiAgICB9KTtcclxuXHJcbn0pKExheW91dEVkaXRvciB8fCAoTGF5b3V0RWRpdG9yID0ge30pKTtcclxuIl0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
