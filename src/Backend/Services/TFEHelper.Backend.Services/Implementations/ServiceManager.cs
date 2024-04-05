using Microsoft.Extensions.Logging;
using TFEHelper.Backend.Services.Abstractions.Interfaces;

namespace TFEHelper.Backend.Services.Implementations
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly ILogger<ServiceManager> _logger;
        private readonly IConfigurationService _configurationService;
        private readonly IPublicationService _publicationService;
        private readonly IPluginService _pluginService;

        IConfigurationService IServiceManager.Configurations => _configurationService;
        IPublicationService IServiceManager.Publications => _publicationService;
        IPluginService IServiceManager.Plugins => _pluginService;

        public ServiceManager(ILogger<ServiceManager> logger, IConfigurationService configurationService, IPublicationService publicationService, IPluginService pluginService)
        {
            _logger = logger;
            _configurationService = configurationService;
            _publicationService = publicationService;
            _pluginService = pluginService;
        }
    }
}
