using Microsoft.Extensions.Logging;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Common;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;

namespace TFEHelper.Backend.Services.Implementations.Configuration
{
    public sealed class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(ILogger<ConfigurationService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<EnumerationTableDTO> GetEnumerationTables()
        {
            List<EnumerationTableDTO> result =
            [
                EnumExtensions.Transformer<BibTeXPublicationDTOType>.ToEnumerationDTO(),
                EnumExtensions.Transformer<FileFormatDTOType>.ToEnumerationDTO(),
                EnumExtensions.Transformer<SearchSourceDTOType>.ToEnumerationDTO(),
            ];
            return result;
        }
    }
}
