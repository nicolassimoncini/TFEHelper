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
using TFEHelper.Backend.Plugins.Scopus.Classes;
using TFEHelper.Backend.Plugins.Scopus.DTO;
using TFEHelper.Backend.Plugins.Scopus.Extensions;

namespace TFEHelper.Backend.Plugins.Scopus
{
    public class ScopusPlugin : IPublicationsCollector
    {
        public string Name => "Scopus plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Scopus";

        private ILogger _logger;
        private PluginConfigurationController _config;

        public bool Configure(ILogger logger)
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
            string uri = _config.Get<string>("URI");
            string APIKey = _config.Get<string>("APIKey");
            int defaultPageSize = _config.Get<int>("DefaultPageSize");

            using (var api = new ScopusAPIWrapper(uri, APIKey, _logger))
            {
                api.Setup(searchParameters, defaultPageSize);

                List<ScopusEntryDTO> result = await api.GetAllRecordsAsync(cancellationToken);
                List<PublicationPLG> publications = new List<PublicationPLG>();

                foreach (var record in result)
                {
                    publications.Add(new PublicationPLG()
                    {
                        Key = record.Identifier,
                        Abstract = record.Abstract, 
                        Authors = record.Creator,
                        DOI = record.DOI,
                        ISBN = record.ISBN,
                        ISSN = record.ISSN,
                        Keywords = record.Keywords, 
                        Pages = record.PageRange,
                        Source = SearchSourcePLGType.Scopus,
                        Title = record.Title,
                        Type = StringExtensions.NormalizePublicationType(record.SubTypeDescription).ToPublicationType(),
                        URL = record.URL,
                        Year = Convert.ToDateTime(record.CoverDate).Year
                    });
                }

                return publications;
            };
        }
    }
}
