using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusISBNDTO
    {
        [JsonPropertyName("@_fa")]
        public string Fa { get; set; }

        [JsonPropertyName("$")]
        public string Value { get; set; }
    }
}
