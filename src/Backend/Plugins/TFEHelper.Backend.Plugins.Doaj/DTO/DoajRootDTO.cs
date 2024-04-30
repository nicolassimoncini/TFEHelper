using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajRootDTO
    {
        [JsonPropertyName("total")]
        public int TotalResult { get; set; }

        [JsonPropertyName("page")]
        public int PageIndex { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("timestamp")]
        public string TimeStamp { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("results")]
        public List<DoajRecordDTO> Results { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }
}
