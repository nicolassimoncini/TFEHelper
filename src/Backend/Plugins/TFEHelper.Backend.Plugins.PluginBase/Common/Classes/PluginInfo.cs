using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;

namespace TFEHelper.Backend.Plugins.PluginBase.Common.Classes
{
    public class PluginInfo
    {
        public int Id { get; set; }
        public PluginType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public Version Version { get; set; } = new Version();
        public string Description { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} - v{Version}";
        }
    }
}
