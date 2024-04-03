﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.Services.Plugin
{
    public sealed class PluginService : IPluginService
    {
        private readonly ILogger<PluginService> _logger;
        private readonly IPluginManager _pluginManager;
        private readonly IMapper _mapper;

        public PluginService(ILogger<PluginService> logger, IPluginManager pluginManager, IMapper mapper)
        {
            _logger = logger;
            _pluginManager = pluginManager;
            _mapper = mapper;
        }

        public IEnumerable<PluginInfoDTO> GetAllPlugins()
        {
            var plugins = _pluginManager
                .GetAllPluginContainers()
                .Select(p => p.Info);

            return _mapper.Map<IEnumerable<PluginInfoDTO>>(plugins);
        }

        public IEnumerable<PluginInfoDTO> GetPublicationsCollectorPlugins()
        {
            var plugins = _pluginManager
                .GetPluginContainers<IPublicationsCollector>()
                .Select(p => p.Info);

            return _mapper.Map<IEnumerable<PluginInfoDTO>>(plugins);
        }

        public async Task<IEnumerable<PublicationDTO>> GetPublicationsFromPluginAsync(int pluginId, PublicationsCollectorParametersDTO searchParameters, CancellationToken cancellationToken = default)
        {
            var plugin = _pluginManager.GetPlugin<IPublicationsCollector>(pluginId) ?? throw new Exception($"Plugin Id={pluginId} does not exist in this context!");
            
            var pluginPublications = await plugin.GetPublicationsAsync(_mapper.Map<PublicationsCollectorParameters>(searchParameters), cancellationToken);
            
            return _mapper.Map<IEnumerable<PublicationDTO>>(pluginPublications);
        }
    }
}

#warning al momento de migrar a cebolla, sacar la dependencia domain -> pluginbase y cambiarla por pluginbase -> domain
#warning luego, eliminar publication, publicationscollectorparameters, bibtexpublicationtype, plugintype (moverlo a domain) y searchsourcetype de pluginbase
