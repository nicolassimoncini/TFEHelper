using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Eric.DTO
{
    public class EricRecordDTO
    {
        [JsonPropertyName("author")]
        public List<string> Authors { get; set; }

        [JsonPropertyName("description")]
        public string Abstract { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("isbn")]
        public List<string> ISBN { get; set; }

        [JsonPropertyName("issn")]
        public List<string> ISSN { get; set; }

        [JsonPropertyName("publicationdateyear")]
        public int Date { get; set; }

        [JsonPropertyName("publicationtype")]
        public List<string> Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }
    }
}
