using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusLinkDTO
    {
        [JsonPropertyName("@_fa")]
        public string Fa { get; set; }

        [JsonPropertyName("@ref")]
        public string Ref { get; set; }

        [JsonPropertyName("@href")]
        public string HRef { get; set; }

        [JsonPropertyName("@type")]
        public string Type { get; set; }
    }
}
