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
using TFEHelper.Backend.Plugins.Doaj.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.Doaj.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TFEHelper.Backend.Plugins.Doaj
{
    public class DoajPlugin : IPublicationsCollectorPlugin
    {

        public string Name => "Doaj plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Doaj";

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
            string FormatArticlePages(string startPage, string endPage)
            {
                if (startPage == null && endPage != null) return endPage;
                if (startPage != null && endPage == null) return startPage;
                if (startPage == null && endPage == null) return null;
                return startPage + "-" + endPage;
            }

            var api = new DoajAPIWrapper(_logger, _config);
            var result = await api.GetAllRecordsAsync(searchParameters, cancellationToken);

            var publications = new List<PublicationPLG>();
            foreach (var record in result)
            {
                publications.Add(new PublicationPLG()
                {
                    Abstract = record.Article.Abstract,
                    Authors = record.Article.Authors?.ToString(", ", (x) => { return x.Name; }),
                    DOI = record.Article.Identifiers?.Where(x => x.Type == "doi").FirstOrDefault()?.Id,
                    ISBN = null,
                    ISSN = record.Article.Journal.ISSNs?.ToString(", ", (x) => { return x; }),
                    Key = record.Id,
                    Keywords = record.Article.Keywords?.ToString(", ", (x) => { return x; }),
                    Pages = FormatArticlePages(record.Article.StartPage, record.Article.EndPage),
                    Source = SearchSourcePLGType.Doaj,
                    Title = record.Article.Title,
                    Type = BibTeXPublicationPLGType.Article,
                    URL = record.Article.Links?.Where(x => x.Type == "fulltext").FirstOrDefault()?.URL,
                    Year = Convert.ToInt32(record.Article.Year ?? "0")
                }); ;
            }

            return publications;
        }
    }
}