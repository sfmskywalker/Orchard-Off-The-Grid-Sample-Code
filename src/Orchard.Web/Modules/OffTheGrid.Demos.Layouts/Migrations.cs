using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace OffTheGrid.Demos.Layouts {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            // Define the UserProfilePart.
            ContentDefinitionManager.AlterPartDefinition("UserProfilePart", part => part
                .WithField("FirstName", f => f
                    .OfType("TextField")
                    .WithSetting("TextFieldSettings.Flavor", "Wide")
                    .WithDisplayName("First Name"))
                .WithField("LastName", f => f
                    .OfType("TextField")
                    .WithSetting("TextFieldSettings.Flavor", "Wide")
                    .WithDisplayName("Last Name"))
                .WithField("TwitterHandle", f => f
                    .OfType("TextField")
                    .WithSetting("TextFieldSettings.Flavor", "Wide")
                    .WithDisplayName("Twitter Handle"))
                .WithDescription("Provides additional information about the user.")
                .Attachable());

            // Attach the UserProfilePart to the User content type.
            ContentDefinitionManager.AlterTypeDefinition("User", type => type
                .WithPart("UserProfilePart"));

            return 1;
        }
    }
}