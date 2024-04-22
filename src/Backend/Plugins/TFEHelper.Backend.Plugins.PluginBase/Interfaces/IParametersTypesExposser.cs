using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IParametersTypesExposser
    {
        public GlobalParametersContainer GetParametersTypes();
    }
}
