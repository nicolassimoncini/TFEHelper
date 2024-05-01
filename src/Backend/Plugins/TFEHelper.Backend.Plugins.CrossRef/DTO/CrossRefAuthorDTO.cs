using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefAuthorDTO
    {
        [JsonPropertyName("given")]
        public string Name { get; set; }

        [JsonPropertyName("family")]
        public string Surname { get; set; }

        [JsonPropertyName("sequence")]
        public string Sequence { get; set; }

        [JsonPropertyName("affiliation")]
        public List<CrossRefAffiliationDTO> Affiliation { get; set; }
    }
}
