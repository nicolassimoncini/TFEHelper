﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.Dummy
{
    public class DummyPlugin : IPublicationsCollectorPlugin, IParametersTypesExposser
    {
        public string Name => "Dummy plugin"; 
        public Version Version => new Version(1,0,0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "Test plugin for IPublicationsCollector";

        private ILogger _logger;
        private PluginConfigurationController _config;

        public bool StartUp(ILogger logger)
        {
            _logger = logger;
            _config = new PluginConfigurationController(_logger);

            return true;
        }

        public async Task<GlobalParametersContainer> GetParametersTypesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var container = new GlobalParametersContainer();
                container.CollectionValued.Add("Subjects", "Medicine", "MEDI");
                container.CollectionValued.Add("Subjects", "Engineering", "ENGI");

                return container;
            }, cancellationToken);
        }

        public Task<IEnumerable<PublicationPLG>> GetPublicationsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {

            _logger.LogInformation("Getting publications...");

            var result = new List<PublicationPLG>()
            {
                new PublicationPLG()
                {
                    Abstract = "abstract abstract abstract abstract abstract abstract abstract abstract ",
                    Authors = "author author and author",
                    DOI = "DOI value",
                    ISBN = "ISBN value",
                    ISSN = "ISSN value",
                    Key = "Key value",
                    Keywords = "Keywords value",
                    Pages = "Pages value",
                    Source = SearchSourcePLGType.Manual,
                    Title = "Title value",
                    Type = BibTeXPublicationPLGType.Article,
                    URL = "URL value",
                    Year = 1995
                },
                new PublicationPLG()
                {
                    Abstract = "abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 ",
                    Authors = "author2 author2 and author2",
                    DOI = "DOI value2",
                    ISBN = "ISBN value2",
                    ISSN = "ISSN value2",
                    Key = "Key value2",
                    Keywords = "Keywords value2",
                    Pages = "Pages value2",
                    Source = SearchSourcePLGType.SEDICI,
                    Title = "Title value2",
                    Type = BibTeXPublicationPLGType.Conference,
                    URL = "URL value2",
                    Year = 1998
                },
                new PublicationPLG()
                {
                    Abstract = "abstract3 abstract3 abstract3 abstract3 abstract3 abstract3 abstract3 abstract3",
                    Authors = "author3 author3 and author3",
                    DOI = "DOI value3",
                    ISBN = "ISBN value3",
                    ISSN = "ISSN value3",
                    Key = "Key value3",
                    Keywords = "Keywords value3",
                    Pages = "Pages value3",
                    Source = SearchSourcePLGType.ACMDigitalLibrary,
                    Title = "Title value3",
                    Type = BibTeXPublicationPLGType.InProceedings,
                    URL = "URL value3",
                    Year = 1983
                },
            };

            return Task.Run<IEnumerable<PublicationPLG>>(() => { return result; }, cancellationToken);
        }

        public bool IsOnline()
        {
            return true;
        }
    }
}
