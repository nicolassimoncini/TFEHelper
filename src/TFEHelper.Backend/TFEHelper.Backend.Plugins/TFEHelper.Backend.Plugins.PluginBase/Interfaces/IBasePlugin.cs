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

        bool Configure();
    }
}
#warning meter acá un "default interface method" para ver si se puede ejecutar de forma abstracta en todos los que hereden de IBasePlugin desde PluginManager...
#warning meter un constructor base con ILogger o hacer algo para que lo pueda inyectar PluginManager...
