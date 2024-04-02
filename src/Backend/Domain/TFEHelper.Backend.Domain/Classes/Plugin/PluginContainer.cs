using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Domain.Classes.Plugin
{
    public class PluginContainer
    {
        public PluginInfo Info { get; set; }
        public IBasePlugin Plugin { get; set; }

        public PluginContainer(PluginInfo info, IBasePlugin plugin)
        {
            Info = info;
            Plugin = plugin;
        }
    }
}
