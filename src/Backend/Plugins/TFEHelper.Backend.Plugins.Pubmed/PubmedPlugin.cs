using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Common.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Tools;
using TFEHelper.Backend.Plugins.Pubmed.Classes;
using TFEHelper.Backend.Plugins.Pubmed.Extensions;

namespace TFEHelper.Backend.Plugins.Pubmed
{
    public class PubmedPlugin : IPublicationsCollectorPlugin
    {

        public string Name => "Pubmed plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Pubmed";

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
            var api = new PubmedAPIWrapper(_logger, _config);
            var result = await api.GetAllRecordsAsync(searchParameters, cancellationToken);

            var publications = new List<PublicationPLG>();
            foreach (var article in result.Articles)
            {
                publications.Add(new PublicationPLG()
                {
                    Abstract = article.Citation.Article.Abstracts?.ToString(System.Environment.NewLine, (x) => { return x.Value; }),
                    Authors = article.Citation.Article.Authors.Items?.ToString(", ", (x) => { return x.LastName + " " + x.ForeName; }),
                    DOI = article.Data.ArticleIds.Where(x => x.Type == "doi").FirstOrDefault()?.Value, 
                    ISBN = null,
                    ISSN = article.Citation.Article.Journal.ISSN?.Value,
                    Key = article.Citation.PMID?.Value,
                    Keywords = article.Citation.Keywords?.Items?.ToString(", ", (x) => { return x.Value; }),
                    Pages = article.Citation.Article.Pagination?.MedlinePageNumber,
                    Source = SearchSourcePLGType.PubMed,
                    Title = article.Citation.Article.Title,
                    Type = BibTeXPublicationPLGType.Article,
                    URL = "https://doi.org/".ConcatIfNotNull(article.Data.ArticleIds.Where(x => x.Type == "doi").FirstOrDefault()?.Value),
                    Year = article.Data.History.OrderByDescending(x => x.Year).FirstOrDefault().Year                    
                });
            }

            return publications;
        }
    }
}
