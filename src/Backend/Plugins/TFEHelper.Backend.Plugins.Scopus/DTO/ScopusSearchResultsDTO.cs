using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusSearchResultsDTO
    {
        [JsonPropertyName("opensearch:totalResults")]
        public int TotalResults { get; set; }
        
        [JsonPropertyName("opensearch:startIndex")]
        public int StartIndex { get; set; }
        
        [JsonPropertyName("opensearch:itemsPerPage")]
        public int ItemsPerPage { get; set; }
        
        [JsonPropertyName("opensearch:Query")]
        public ScopusQueryDTO Queries { get; set; }
        
        [JsonPropertyName("link")]
        public List<ScopusLinkDTO> Links { get; set; }
        
        [JsonPropertyName("entry")]
        public List<ScopusEntryDTO> Entries { get; set; }
    }
}
