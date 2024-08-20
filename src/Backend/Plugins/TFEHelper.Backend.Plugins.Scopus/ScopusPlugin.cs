using Microsoft.Extensions.Logging;
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
using TFEHelper.Backend.Plugins.Scopus.Classes;
using TFEHelper.Backend.Plugins.Scopus.DTO;
using TFEHelper.Backend.Plugins.Scopus.Extensions;

namespace TFEHelper.Backend.Plugins.Scopus
{
    public class ScopusPlugin : IPublicationsCollectorPlugin, IParametersTypesExposser
    {
    
        public string Name => "Scopus plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Scopus";

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
            string uri = _config.Get<string>("SearchURI");
            string APIKey = _config.Get<string>("APIKey");
            int defaultPageSize = _config.Get<int>("DefaultPageSize");

            using (var api = new ScopusAPIWrapper(uri, APIKey, _logger))
            {
                List<ScopusEntryDTO> result = await api.GetAllRecordsAsync(searchParameters, defaultPageSize, cancellationToken);
                List<PublicationPLG> publications = new List<PublicationPLG>();

                foreach (var record in result)
                {
                    publications.Add(new PublicationPLG()
                    {
                        Key = record.Identifier,
                        Abstract = record.Abstract, 
                        Authors = record.Creator,
                        DOI = record.DOI,
                        ISBN = record.ISBN?.ToString(", ", (x) => { return x.Value; }),
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

        public async Task<GlobalParametersContainer> GetParametersTypesAsync(CancellationToken cancellationToken = default)
        {
            string uri = _config.Get<string>("SubjectsURI");
            string APIKey = _config.Get<string>("APIKey");
            var parameters = new GlobalParametersContainer();

            using (var api = new ScopusAPIWrapper(uri, APIKey, _logger))
            {
                ScopusSubjectRootDTO root = await api.GetAllSubjectsAsync(cancellationToken);

                foreach (var subject in root.Info.Subjects)
                {
                    parameters.CollectionValued.Add("Subjects", subject.Detail, subject.Abbreviature);
                }
            }

            return parameters;
        }
    }
}