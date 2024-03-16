using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IPublicationsCollector : IBasePlugin
    {
        bool Configure();
        bool IsOnline();
        Task<IEnumerable<Publication>> GetPublicationsAsync(string searchQuery, CancellationToken cancellationToken = default);
    }
}
