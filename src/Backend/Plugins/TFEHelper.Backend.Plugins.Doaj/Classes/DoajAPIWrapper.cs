using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.Doaj.DTO;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.Doaj.Classes
{
    internal class DoajAPIWrapper : IDisposable
    {
        private readonly PluginConfigurationController _config;
        private RestClient _client;
        private RestRequest _request;
        private ILogger _logger;

        public DoajAPIWrapper(ILogger logger, PluginConfigurationController config)
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
            const string PUB_DATE_FMT = "(bibjson.year:>={0} AND bibjson.year:<={1})";
            string yearRange;
            int yearFrom = searchParameters.DateFrom.Year;
            int yearTo = searchParameters.DateTo.Year;

            yearRange = string.Format(PUB_DATE_FMT, yearFrom, yearTo);

            return searchParameters.Query + " " + yearRange;
        }


        public async Task<List<DoajRecordDTO>> GetAllRecordsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var pollingDelayInMs = _config.Get<int>("PollingDelayInMs");
            var uri = _config.Get<string>("SearchURI");
            var maxQuantitySupported = _config.Get<int>("MaxQuantityResult");
            var pageSize = _config.Get<int>("DefaultPageSize");
            int remaining;
            int pageIndex = 1;
            bool maxReached = false;
            List<DoajRecordDTO> records = new List<DoajRecordDTO>();
            DoajRootDTO result = new DoajRootDTO();

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
            _request.Resource = BuildSearchQuery(searchParameters);
            _request.AddQueryParameter("page", pageIndex);
            _request.AddQueryParameter("pageSize", pageSize);

            using (_client = new RestClient(uri))
            {
                do
                {
                    result = await _client.GetAsync<DoajRootDTO>(_request, cancellationToken);

                    if (result.TotalResult == 0 || result.Results == null || result.Results.Count == 0) break;

                    records.AddRange(result.Results.GetRange(0, Math.Min(remaining, result.Results.Count)));
                    remaining -= Math.Min(remaining, result.Results.Count);
                    maxReached = (remaining == 0);

                    _logger.LogInformation($"Retrieved {records.Count} / {searchParameters.ReturnQuantityLimit} of {result.TotalResult} total records.");

                    if (remaining > 0)
                    {
                        pageIndex++;
                        _request.Parameters.RemoveParameter("page");
                        _request.AddQueryParameter("page", pageIndex);

                        await Task.Delay(pollingDelayInMs, cancellationToken);
                    }
                } while (!maxReached);                
            }

            return records;
        }
    }
}

// Documentation: https://doaj.org/api/docs#!/Search/get_api_search_articles_search_query
// Ejemplo: https://doaj.org/api/v3/search/articles/software%20development?page=1&pageSize=10