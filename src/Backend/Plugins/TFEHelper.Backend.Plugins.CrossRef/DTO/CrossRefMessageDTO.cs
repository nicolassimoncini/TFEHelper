using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefMessageDTO
    {
        [JsonPropertyName("total-results")]
        public int TotalResult { get; set; }

        [JsonPropertyName("items")]
        public List<CrossRefArticleDTO> Articles { get; set; }

        [JsonPropertyName("items-per-page")]
        public int ItemsPerPage { get; set; }

        [JsonPropertyName("query")]
        public CrossRefQueryDTO Query { get; set; }
    }
}
