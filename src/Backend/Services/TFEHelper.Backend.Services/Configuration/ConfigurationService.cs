using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;
using TFEHelper.Backend.Services.Contracts.Extensions;

namespace TFEHelper.Backend.Services.Configuration
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
