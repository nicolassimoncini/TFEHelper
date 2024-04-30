using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajLinkDTO
    {
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }
    }
}
