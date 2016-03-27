using System;
using System.Collections.Generic;
using System.Linq;
using OffTheGrid.Demos.Layouts.Elements;
using Orchard;
using Orchard.ContentManagement.MetaData;
using Orchard.Environment;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Framework.Harvesters;
using Orchard.Layouts.Services;

namespace OffTheGrid.Demos.Layouts
{
    public class UserProfileElementHarvester : Component, IElementHarvester
    {
        private readonly Work<IContentDefinitionManager> _contentDefinitionManager;
        private readonly Work<IContentFieldDisplay> _contentFieldDisplay;
        private readonly IWorkContextAccessor _workContextAccesor;

        public UserProfileElementHarvester(
           Work<IContentDefinitionManager> contentDefinitionManager,
           Work<IContentFieldDisplay> contentFieldDisplay,
           IWorkContextAccessor workContextAccesor) {

            _contentDefinitionManager = contentDefinitionManager;
            _workContextAccesor = workContextAccesor;
            _contentFieldDisplay = contentFieldDisplay;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context)
        {
            // Get the UserProfilePart definition.
            var partDefinition =
               _contentDefinitionManager
               .Value
               .GetPartDefinition("UserProfilePart");

            // Get the content fields from the UserProfilePart definition.
            var fieldDefinitions = partDefinition.Fields;

            // For each field, yield an element descriptor.
            return from field in fieldDefinitions
                   let settingKeys = field.Settings.Keys
                   let descriptionKey = settingKeys.FirstOrDefault(x => x.IndexOf("description", StringComparison.OrdinalIgnoreCase) >= 0)
                   let description = descriptionKey != null ? field.Settings[descriptionKey] : $"The {field.DisplayName} field."
                   select new ElementDescriptor(
                       elementType: typeof(UserProfileField),
                       typeName: $"UserProfile.{field.Name}",
                       displayText: T(field.DisplayName),
                       description: T(description),
                       category: "User"
                   )
                   {
                       ToolboxIcon = "\uf040",
                       Displaying = displayingContext => OnDisplaying(field.Name, displayingContext)
                   };
        }

        private void OnDisplaying(string fieldName, ElementDisplayingContext context)
        {
            var workContext = _workContextAccesor.GetContext();
            var currentUser = workContext.CurrentUser;
            var profilePart = currentUser?.ContentItem.Parts.SingleOrDefault(x => x.PartDefinition.Name == "UserProfilePart");
            var field = profilePart?.Fields.SingleOrDefault(x => x.Name == fieldName);

            if (field == null)
            {
                // The field is no longer part the UserProfilePart.
                // This situation can occur when a user removed the field
                // and the harvested element descriptors cache entry hasn't been
                // evicted yet.
                return;
            }

            // Render the field.
            var contentFieldShapeHolder = _contentFieldDisplay.Value.BuildDisplay(currentUser, field, context.DisplayType);

            // The returned shape is a ContentField shape that has a Content property of type ZoneHolding,
            // which in turn contains a single shape which is the field shape we're interested in.
            var fieldShapes = ((IEnumerable<dynamic>)contentFieldShapeHolder.Content.Items);

            // Add alternates to each content field shape.
            foreach (var fieldShape in fieldShapes)
            {
                fieldShape.Metadata.Alternates.Add($"{fieldShape.Metadata.Type}__UserProfile");
                fieldShape.Metadata.Alternates.Add($"{fieldShape.Metadata.Type}__UserProfile__{field.Name}");
            }

            // Assign the field shape to a new property of the element shape.
            context.ElementShape.ContentField = contentFieldShapeHolder;
        }

    }
}
