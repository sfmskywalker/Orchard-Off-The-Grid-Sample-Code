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
