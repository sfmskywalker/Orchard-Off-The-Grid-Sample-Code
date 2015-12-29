using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Orchard;

namespace OffTheGrid.Demos.Layouts.Models {

    public class SlidesSerializer : Component, ISlidesSerializer {
        public string Serialize(IEnumerable<Slide> value) {
            return JsonConvert.SerializeObject(value.ToList());
        }

        public IEnumerable<Slide> Deserialize(string value) {
            if (String.IsNullOrWhiteSpace(value))
                return Enumerable.Empty<Slide>();

            return JsonConvert.DeserializeObject<List<Slide>>(value);
        }
    }
}