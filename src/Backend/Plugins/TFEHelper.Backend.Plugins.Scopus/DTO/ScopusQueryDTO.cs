using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusQueryDTO
    {
        [JsonPropertyName("@role")]
        public string Role { get; set; }

        [JsonPropertyName("@searchTerms")]
        public string SearchTerms { get; set; }

        [JsonPropertyName("@startPage")]
        public string StartPage { get; set; }
    }
}
