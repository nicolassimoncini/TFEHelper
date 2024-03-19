using System;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Domain.Classes.Models
{
    public class PluginInfo
    {
        public int Id { get; set; }
        public PluginType Type { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} - v{Version}";
        }
    }
}
