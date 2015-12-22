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
