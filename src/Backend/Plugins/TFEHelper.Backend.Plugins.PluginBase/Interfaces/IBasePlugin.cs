using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Enums;

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