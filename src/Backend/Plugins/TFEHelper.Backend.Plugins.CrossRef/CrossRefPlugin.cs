using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Tools;
using TFEHelper.Backend.Plugins.CrossRef.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using System.Linq;
using System.Runtime.CompilerServices;
using TFEHelper.Backend.Plugins.CrossRef.Extensions;

namespace TFEHelper.Backend.Plugins.CrossRef
{
    public class CrossRefPlugin : IPublicationsCollectorPlugin
    {

        public string Name => "CrossRef plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for CrossRef";

        private ILogger _logger;
        private PluginConfigurationController _config;

        public bool StartUp(ILogger logger)
        {
            _logger = logger;
            _config = new PluginConfigurationController(_logger);

            return true;
        }

        public bool IsOnline()
        {
            return true;
        }

        public async Task<IEnumerable<PublicationPLG>> GetPublicationsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var api = new CrossRefAPIWrapper(_logger, _config);
            var result = await api.GetAllRecordsAsync(searchParameters, cancellationToken);

            var publications = new List<PublicationPLG>();
            foreach (var record in result)
            {
                publications.Add(new PublicationPLG()
                {
                    Abstract = record.Abstract,
                    Authors = record.Authors?.ToString(", ", (x) => { return x.Surname + " " + x.Name; }),
                    DOI = record.DOI,
                    ISBN = record.ISBNs?.ToString(", ", (x) => { return x; }),
                    ISSN = record.ISSNs?.ToString(", ", (x) => { return x; }),
                    Key = null,
                    Keywords = null,
                    Pages = record.Page,
                    Source = SearchSourcePLGType.CrossRef,
                    Title = record.Titles?.FirstOrDefault(),
                    Type = BibTeXPublicationPLGType.Article,
                    URL = record.URL,
                    Year = record.Published?.Dates?.FirstOrDefault()?.Max() ?? 0
                });
            }

            return publications;
        }
    }
}