using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Core.Plugin.Interfaces
{
    public interface IPluginManager
    {
        void Scan();
        IEnumerable<T> GetPlugins<T>() where T : IBasePlugin;
    }
}
