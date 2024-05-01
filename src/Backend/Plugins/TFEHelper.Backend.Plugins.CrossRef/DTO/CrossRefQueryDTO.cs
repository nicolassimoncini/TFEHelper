using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefQueryDTO
    {
        [JsonPropertyName("start-index")]
        public int StartIndex { get; set; }

        [JsonPropertyName("search-terms")]
        public string SearchTerms { get; set; }
    }
}
