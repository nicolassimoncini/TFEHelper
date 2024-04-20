using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusRootDTO
    {
        [JsonPropertyName("search-results")]
        public ScopusSearchResultsDTO Result { get; set; }
    }
}
