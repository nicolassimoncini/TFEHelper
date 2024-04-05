using Microsoft.Extensions.Logging;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IBasePlugin
    {
        public string Name { get; }
        public Version Version { get; }
        public PluginType Type { get; }
        public string Description { get; }

        bool Configure(ILogger logger);
    }
}