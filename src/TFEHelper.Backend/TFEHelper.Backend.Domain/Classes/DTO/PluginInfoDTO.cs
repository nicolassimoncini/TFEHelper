using TFEHelper.Backend.Plugins.PluginBase.Enums;

namespace TFEHelper.Backend.Domain.Classes.DTO
{
    public class PluginInfoDTO
    {
        public int Id { get; set; }
        public PluginType Type { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}
