using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefArticleDTO
    {
        [JsonPropertyName("DOI")]
        public string DOI { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("page")]
        public string Page { get; set; }

        [JsonPropertyName("title")]
        public List<string> Titles { get; set; }

        [JsonPropertyName("author")]
        public List<CrossRefAuthorDTO> Authors { get; set; }

        [JsonPropertyName("ISBN")]
        public List<string> ISBNs { get; set; }

        [JsonPropertyName("ISSN")]
        public List<string> ISSNs { get; set; }

        [JsonPropertyName("URL")]
        public string URL { get; set; }

        [JsonPropertyName("published")]
        public CrossRefPublishedDTO Published { get; set; }

        [JsonPropertyName("abstract")]
        public string Abstract { get; set; }
    }
}