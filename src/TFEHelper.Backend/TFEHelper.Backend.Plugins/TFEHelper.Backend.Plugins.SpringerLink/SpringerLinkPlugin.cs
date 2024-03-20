using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Plugins.SpringerLink
{
    public class SpringerLinkPlugin : IPublicationsCollector
    {
        public string Name => "SpringerLink plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Springer Link";

        public bool Configure()
        {
            return true;
        }

        public bool IsOnline()
        {
            return true;
        }

        public Task<IEnumerable<Publication>> GetPublicationsAsync(SearchParameters searchParameters, CancellationToken cancellationToken = default)
        {
            return Task.Run<IEnumerable<Publication>>(() => { return new List<Publication>(); }, cancellationToken);
        }
    }
}
