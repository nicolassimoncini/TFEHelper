using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Eric.DTO
{
    public class EricRootDTO
    {
        [JsonPropertyName("response")]
        public EricSearchResultDTO Result { get; set; }
    }
}
