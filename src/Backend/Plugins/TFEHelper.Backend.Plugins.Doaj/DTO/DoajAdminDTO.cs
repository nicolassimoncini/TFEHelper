using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.Doaj.DTO
{
    public class DoajAdminDTO
    {
        [JsonPropertyName("seal")]
        public bool Seal { get; set; }
    }
}
