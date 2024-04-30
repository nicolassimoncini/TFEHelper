using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajAuthorDTO
    {
        [JsonPropertyName("orcid_id")]
        public string OrcidId { get; set; }

        [JsonPropertyName("affiliation")]
        public string Affiliation { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
