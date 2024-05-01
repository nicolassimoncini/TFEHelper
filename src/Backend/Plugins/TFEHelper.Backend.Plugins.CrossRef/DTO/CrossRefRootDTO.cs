using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.CrossRef.DTO
{
    public class CrossRefRootDTO
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message-type")]
        public string MessageType { get; set; }

        [JsonPropertyName("message-version")]
        public string MessageVersion { get; set; }

        [JsonPropertyName("message")]
        public CrossRefMessageDTO Message { get; set; }
    }
}
