using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistant.LightController.Models {
    internal class LightPostBody {

        public LightPostBody(string entity_id) {
            this.entity_id = entity_id;
        }

        public string entity_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? brightness_pct { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? transition { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int[] rgb_color { get; set; }

        public HttpContent ToContent() {
            string stringPayload = JsonConvert.SerializeObject(this);
            return new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
    }

}
