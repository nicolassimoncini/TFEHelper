using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.CrossRef.DTO;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.CrossRef.Classes
{
    internal class CrossRefAPIWrapper : IDisposable
    {
        private readonly PluginConfigurationController _config;
        private RestClient _client;
        private RestRequest _request;
        private ILogger _logger;

        public CrossRefAPIWrapper(ILogger logger, PluginConfigurationController config)
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

        private string BuildSearchQuery(PublicationsCollectorParametersPLG searchParameters)
        {
            return searchParameters.Query;
        }

        private string BuildSearchFilters(PublicationsCollectorParametersPLG searchParameters)
        {
            const string PUB_DATE_FMT = "from-pub-date:{0:yyyy-MM-dd},until-pub-date:{1:yyyy-MM-dd}";
            return string.Format(PUB_DATE_FMT, searchParameters.DateFrom, searchParameters.DateTo);
        }


        public async Task<List<CrossRefArticleDTO>> GetAllRecordsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var pollingDelayInMs = _config.Get<int>("PollingDelayInMs");
            var uri = _config.Get<string>("SearchURI");
            var maxQuantitySupported = _config.Get<int>("MaxQuantityResult");
            var pageSize = _config.Get<int>("DefaultPageSize");
            int remaining;
            int pageIndex = 0;
            bool maxReached = false;
            List<CrossRefArticleDTO> records = new List<CrossRefArticleDTO>();
            CrossRefRootDTO result = new CrossRefRootDTO();

            if (searchParameters.ReturnQuantityLimit > maxQuantitySupported)
            {
                _logger.LogInformation("Too many records for the request.  Capping to {maxQuantityResult}...", maxQuantitySupported);
                remaining = maxQuantitySupported;
                searchParameters.ReturnQuantityLimit = maxQuantitySupported;
            }
            else remaining = searchParameters.ReturnQuantityLimit;

            _logger.LogInformation("Requesting {ReturnQuantityLimit} publications...", searchParameters.ReturnQuantityLimit);

            if (pageSize > remaining) pageSize = remaining;

            _request = new RestRequest();
            _request.AddQueryParameter("query", BuildSearchQuery(searchParameters));
            _request.AddQueryParameter("filter", BuildSearchFilters(searchParameters));
            _request.AddQueryParameter("select", "DOI,ISSN,ISBN,URL,abstract,author,page,published,title,type");
            _request.AddQueryParameter("offset", pageIndex);
            _request.AddQueryParameter("rows", pageSize);

            using (_client = new RestClient(uri))
            {
                do
                {
                    result = await _client.GetAsync<CrossRefRootDTO>(_request, cancellationToken);

                    if (result.Message.TotalResult == 0 || result.Message == null || result.Message.Articles.Count == 0) break;

                    records.AddRange(result.Message.Articles.GetRange(0, Math.Min(remaining, result.Message.Articles.Count)));
                    remaining -= Math.Min(remaining, result.Message.Articles.Count);
                    maxReached = (remaining == 0);

                    _logger.LogInformation($"Retrieved {records.Count} / {searchParameters.ReturnQuantityLimit} of {result.Message.TotalResult} total records.");

                    if (remaining > 0)
                    {
                        pageIndex += pageSize;
                        _request.Parameters.RemoveParameter("offset");
                        _request.AddQueryParameter("offset", pageIndex);

                        await Task.Delay(pollingDelayInMs, cancellationToken);
                    }
                } while (!maxReached);
            }

            return records;
        }
    }
}
// Documentation: https://api.crossref.org/swagger-ui/index.html#/Works/get_works
// Ejemplo: https://api.crossref.org/works?rows=100&offset=0&mailto=nico10k%40hotmail.com&query=software%20development&select=DOI,ISSN,ISBN,URL,abstract,author,page,published,title,type&filter=from-pub-date:2010-10-02,until-pub-date:2020-01-01