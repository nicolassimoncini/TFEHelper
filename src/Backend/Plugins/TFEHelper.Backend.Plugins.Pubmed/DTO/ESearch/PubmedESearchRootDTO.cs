using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.ESearch
{
    public class PubmedESearchRootDTO
    {
        [JsonPropertyName("header")]
        public PubmedESearchHeadDTO Head { get; set; }

        [JsonPropertyName("esearchresult")]
        public PubmedESearchResultDTO Result { get; set; }
    }
}
