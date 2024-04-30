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
using TFEHelper.Backend.Plugins.Eric.Classes;
using TFEHelper.Backend.Plugins.Eric.Extensions;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;

namespace TFEHelper.Backend.Plugins.Eric
{
    public class EricPlugin : IPublicationsCollectorPlugin
    {

        public string Name => "Eric plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Eric";

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
            var api = new EricAPIWrapper(_logger, _config);
            var result = await api.GetAllRecordsAsync(searchParameters, cancellationToken);

            var publications = new List<PublicationPLG>();
            foreach (var article in result)
            {
                publications.Add(new PublicationPLG()
                {
                    Abstract = article.Abstract,
                    Authors = article.Authors?.ToString(", ", (x) => { return x; }),
                    DOI = null,
                    ISBN = article.ISBN?.ToString(", ", (x) => { return x; }),
                    ISSN = article.ISSN?.ToString(", ", (x) => { return x; }),
                    Key = article.Id,
                    Keywords = null,
                    Pages = null,
                    Source = SearchSourcePLGType.Eric,
                    Title = article.Title,
                    Type = BibTeXPublicationPLGType.Article,
                    URL = article.URL,
                    Year = article.Date
                });
            }

            return publications;
        }
    }
}