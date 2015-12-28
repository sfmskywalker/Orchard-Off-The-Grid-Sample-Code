using System.Collections.Generic;
using Orchard;

namespace OffTheGrid.Demos.Layouts.Models {
    public interface ISlidesSerializer : IDependency {
        string Serialize(IEnumerable<Slide> value);
        IEnumerable<Slide> Deserialize(string value);
    }
}