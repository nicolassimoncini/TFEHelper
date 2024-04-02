using System;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Domain.Classes.Plugin
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
