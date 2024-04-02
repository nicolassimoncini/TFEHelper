using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.Services.Abstractions.Interfaces
{
    public interface IPluginService
    {
        IEnumerable<PluginInfoDTO> GetAllPlugins();
        IEnumerable<PluginInfoDTO> GetPublicationsCollectorPlugins();
        Task<IEnumerable<PublicationDTO>> GetPublicationsFromPluginAsync(int pluginId, PublicationsCollectorParametersDTO searchParameters, CancellationToken cancellationToken = default);
    }
}
