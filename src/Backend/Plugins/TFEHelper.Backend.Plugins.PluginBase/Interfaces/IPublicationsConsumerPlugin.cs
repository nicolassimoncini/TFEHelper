using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    internal interface IPublicationsConsumerPlugin : IBasePlugin
    {
        Task<bool> SendPublicationsAsync(IEnumerable<PublicationPLG> publications, CancellationToken cancellationToken = default);
    }
}
