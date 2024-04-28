using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Serializers.Xml;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Tools;
using TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch;
using TFEHelper.Backend.Plugins.Pubmed.DTO.ESearch;

namespace TFEHelper.Backend.Plugins.Pubmed.Classes
{
    internal class PubmedAPIWrapper: IDisposable
    {
        private readonly PluginConfigurationController _config;
        private RestClient _client;
        private RestRequest _request;
        private ILogger _logger;

        public PubmedAPIWrapper(ILogger logger, PluginConfigurationController config)
        {
            _logger = logger;
            _config = config;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _logger = null;
            _client?.Dispose();
        }

        public async Task<PubmedEFetchRootDTO> GetAllRecordsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var eSearchURI = _config.Get<string>("ESearchURI");
            var eFetchURI = _config.Get<string>("EFetchURI");
            var maxQuantityResult = _config.Get<int>("MaxQuantityResult");
            PubmedESearchRootDTO eSearchResult = new PubmedESearchRootDTO();
            PubmedEFetchRootDTO eFetchResult = new PubmedEFetchRootDTO();

            if (searchParameters.ReturnQuantityLimit > maxQuantityResult)
            {
                _logger.LogInformation("Too many records for the request.  Capping to {maxQuantityResult}...", maxQuantityResult);
                searchParameters.ReturnQuantityLimit = maxQuantityResult;
            }

            _logger.LogInformation("Requesting {ReturnQuantityLimit} publications indexes...", searchParameters.ReturnQuantityLimit);
            using (_client = new RestClient(eSearchURI))
            {
                _request = new RestRequest("", method: Method.Post);
                _request.AddQueryParameter("db", "pubmed");
                _request.AddQueryParameter("retsart", 0);
                _request.AddQueryParameter("retmax", searchParameters.ReturnQuantityLimit);
                _request.AddQueryParameter("retmode", "json");
                _request.AddQueryParameter("term", searchParameters.Query);
                _request.AddQueryParameter("field", "title/abstract");
                _request.AddQueryParameter("datetype", "pdat");
                _request.AddQueryParameter("mindate", string.Format("{0:yyyy/MM/dd}", searchParameters.DateFrom));
                _request.AddQueryParameter("maxdate", string.Format("{0:yyyy/MM/dd}", searchParameters.DateTo));

                eSearchResult = await _client.PostAsync<PubmedESearchRootDTO>(_request, cancellationToken);
            }

            if (eSearchResult is not null && eSearchResult.Result?.TotalResults != 0)
            {
                _logger.LogInformation("Requesting {ReturnQuantityLimit} publications data of {TotalResult}...", searchParameters.ReturnQuantityLimit, eSearchResult.Result.TotalResults);

                using (_client = new RestClient(eFetchURI, configureSerialization: x => { x.UseSerializer(() => new XMLCustomSerializer()); }))
                {
                    _request = new RestRequest("", method: Method.Post);
                    _request.RequestFormat = DataFormat.Xml;
                    _request.AddQueryParameter("db", "pubmed");
                    _request.AddQueryParameter("retsart", 0);
                    _request.AddQueryParameter("retmax", searchParameters.ReturnQuantityLimit);
                    _request.AddQueryParameter("retmode", "xml");
                    _request.AddQueryParameter("id", string.Join(",", eSearchResult.Result.IdList.Select(x => x)));

                    eFetchResult = await _client.PostAsync<PubmedEFetchRootDTO>(_request, cancellationToken);
                }
            }

            return eFetchResult;
        }
    }
}