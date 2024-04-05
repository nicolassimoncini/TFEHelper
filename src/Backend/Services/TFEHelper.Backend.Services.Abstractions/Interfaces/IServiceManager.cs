using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Abstractions.Interfaces
{
    public interface IServiceManager
    {
        IConfigurationService Configurations { get; }
        IPublicationService Publications { get; }
        IPluginService Plugins { get; }
    }
}
