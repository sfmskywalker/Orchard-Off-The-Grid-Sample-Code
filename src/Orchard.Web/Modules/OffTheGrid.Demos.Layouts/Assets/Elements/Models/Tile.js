var LayoutEditor;
(function (LayoutEditor) {

    LayoutEditor.Tile = function (data, htmlId, htmlClass, htmlStyle, isTemplated, rule, hasEditor, contentType, contentTypeLabel, contentTypeClass, children) {
        // Inherit from the Element base class.
        LayoutEditor.Element.call(this, "Tile", data, htmlId, htmlClass, htmlStyle, isTemplated, rule);

        // Inherit from the Container base class.
        LayoutEditor.Container.call(this, ["Canvas", "Grid", "Content"], children);

        // This Tile element is containable, which means it can be added to any container, including Tiles.
        this.isContainable = true;

        this.hasEditor = hasEditor;
        this.contentType = contentType;
        this.contentTypeLabel = contentTypeLabel;
        this.contentTypeClass = contentTypeClass;

        // Implements the toObject serialization function.
        this.toObject = function () {
            var result = this.elementToObject();
            result.children = this.childrenToObject();
            return result;
        };
    };

    // Implements the factory function invoked by the element factory.
    LayoutEditor.Tile.from = function (value) {
        var result = new LayoutEditor.Tile(
            value.data,
            value.htmlId,
            value.htmlClass,
            value.htmlStyle,
            value.isTemplated,
            value.rule,
            value.hasEditor,
            value.contentType,
            value.contentTypeLabel,
            value.contentTypeClass,
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
