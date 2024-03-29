﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IPublicationsCollector : IBasePlugin
    {
        bool IsOnline();
        Task<IEnumerable<Publication>> GetPublicationsAsync(PublicationsCollectorParameters searchParameters, CancellationToken cancellationToken = default);
    }
}
