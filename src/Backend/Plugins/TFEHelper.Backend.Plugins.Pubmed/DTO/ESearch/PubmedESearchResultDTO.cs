using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.ESearch
{
    public class PubmedESearchResultDTO
    {
        [JsonPropertyName("count")]
        public int TotalResults { get; set; }

        [JsonPropertyName("retmax")]
        public int TotalRequested { get; set; }

        [JsonPropertyName("retstart")]
        public int StartIndex { get; set; }

        [JsonPropertyName("idlist")]
        public List<string> IdList { get; set; }

        [JsonPropertyName("translationset")]
        public List<PubmedESearchTranslation> Translations { get; set; }

        [JsonPropertyName("querytranslation")]
        public string QueryTranslation { get; set; }
    }
}
