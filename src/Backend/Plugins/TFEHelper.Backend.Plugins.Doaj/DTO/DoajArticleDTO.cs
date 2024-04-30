using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajArticleDTO
    {
        [JsonPropertyName("identifier")]
        public List<DoajIdentifierDTO> Identifiers { get; set; }

        [JsonPropertyName("journal")]
        public DoajJournalDTO Journal { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("keywords")]
        public List<string> Keywords { get; set; }

        [JsonPropertyName("start_page")]
        public string StartPage { get; set; }

        [JsonPropertyName("end_page")]
        public string EndPage { get; set; }

        [JsonPropertyName("subject")]
        public List<DoajSubjectDTO> Subjects { get; set; }

        [JsonPropertyName("author")]
        public List<DoajAuthorDTO> Authors { get; set; }

        [JsonPropertyName("link")]
        public List<DoajLinkDTO> Links { get; set; }

        [JsonPropertyName("abstract")]
        public string Abstract { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
