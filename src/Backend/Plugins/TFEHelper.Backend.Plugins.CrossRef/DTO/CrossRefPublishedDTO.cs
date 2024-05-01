using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefPublishedDTO
    {
        [JsonPropertyName("date-parts")]
        public List<List<int>> Dates { get; set; }
    }
}
