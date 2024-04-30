using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajRecordDTO
    {
        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("bibjson")]
        public DoajArticleDTO Article { get; set; }

        [JsonPropertyName("admin")]
        public DoajAdminDTO Admin { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created_date")]
        public string CreatedDate { get; set; }
    }
}
