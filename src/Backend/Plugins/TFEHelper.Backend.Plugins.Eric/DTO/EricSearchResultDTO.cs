using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Eric.DTO
{
    public class EricSearchResultDTO
    {
        [JsonPropertyName("numFound")]
        public int TotalResult { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("numFoundExact")]
        public bool FoundExact { get; set; }

        [JsonPropertyName("docs")]
        public List<EricRecordDTO> Documents { get; set; } = new List<EricRecordDTO>();
    }
}
