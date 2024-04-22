using System;

namespace TFEHelper.Backend.Services.Contracts.DTO.Plugin
{
    public class PluginInfoDTO
    {
        public int Id { get; set; }
        public PluginDTOType Type { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Description { get; set; }
        public dynamic Parameters { get; set; }

        public override string ToString()
        {
            return $"{Name} - v{Version}";
        }
    }
}
