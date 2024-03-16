using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IBasePlugin
    {
        public string Name { get; }
        public string Description { get; }
    }
}
