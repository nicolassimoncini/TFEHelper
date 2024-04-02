using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Abstractions.Interfaces
{
    public interface IServiceManager
    {
        IConfigurationService ConfigurationService { get; }
        IPublicationService PublicationService { get; }
        IPluginService PluginService { get; }
    }
}
