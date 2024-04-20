using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Scopus.DTO
{
    public class ScopusConfigContainerDTO
    {
        [JsonPropertyName("value")]
        public List<ScopusConfigItemDTO> Items { get; set; }
    }
}
