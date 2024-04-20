using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusAffiliationDTO
    {
        [JsonPropertyName("@_fa")]
        public string Fa { get; set; }

        [JsonPropertyName("affilname")]
        public string Name { get; set; }

        [JsonPropertyName("affiliation-city")]
        public string City { get; set; }

        [JsonPropertyName("affiliation-country")]
        public string Country { get; set; }
    }
}
