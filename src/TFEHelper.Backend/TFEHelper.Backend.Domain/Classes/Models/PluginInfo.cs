using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Classes.DTO
{
    public class PluginInfo
    {
        public PluginType Type { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} - v{Version}";
        }
    }
}
