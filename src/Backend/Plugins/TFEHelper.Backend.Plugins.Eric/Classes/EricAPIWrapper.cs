using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.Eric.DTO;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.Eric.Classes
{
    internal class EricAPIWrapper : IDisposable
    {
        private readonly PluginConfigurationController _config;
        private RestClient _client;
        private RestRequest _request;
        private ILogger _logger;

        public EricAPIWrapper(ILogger logger, PluginConfigurationController config)
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
            const string PUB_DATE_FMT = "publicationdateyear:{0}";
            string yearRange;
            int yearFrom = searchParameters.DateFrom.Year;
            int yearTo = searchParameters.DateTo.Year;

            if (yearFrom == yearTo)
            {
                yearRange = string.Format(PUB_DATE_FMT, yearFrom);
            }
            else
            {
                yearRange = " AND (";
                for (var i = yearFrom;  i <= yearTo; i++)
                {
                    yearRange += string.Format(PUB_DATE_FMT, i);
                    if (i < yearTo) yearRange += " OR ";
                };
                yearRange += ")";
            }            

            return searchParameters.Query + yearRange;
        }


        public async Task<List<EricRecordDTO>> GetAllRecordsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default)
        {
            var pollingDelayInMs = _config.Get<int>("PollingDelayInMs");
            var uri = _config.Get<string>("SearchURI");
            var maxQuantitySupported = _config.Get<int>("MaxQuantityResult");
            var pageSize = _config.Get<int>("DefaultPageSize");
            int remaining;
            int startIndex = 0;
            bool maxReached = false;
            List<EricRecordDTO> records = new List<EricRecordDTO>();
            EricRootDTO result = new EricRootDTO();

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
            _request.AddQueryParameter("search", BuildSearchQuery(searchParameters));
            _request.AddQueryParameter("format", "json");            
            _request.AddQueryParameter("fields", "author,description,id,isbn,issn,publicationdateyear,publicationtype,title,url");
            _request.AddQueryParameter("start", startIndex);
            _request.AddQueryParameter("rows", pageSize);

            using (_client = new RestClient(uri))
            {
                do
                {
                    result = await _client.GetAsync<EricRootDTO>(_request, cancellationToken);

                    if (result.Result.TotalResult == 0 || result.Result.Documents == null || result.Result.Documents.Count == 0) break;

                    records.AddRange(result.Result.Documents);
                    remaining -=  result.Result.Documents.Count;
                    maxReached = (remaining == 0);

                    _logger.LogInformation($"Retrieved {records.Count} / {searchParameters.ReturnQuantityLimit} of {result.Result.TotalResult} total records.");

                    if (remaining > 0)
                    {
                        startIndex += pageSize +1;
                        _request.Parameters.RemoveParameter("start");
                        _request.AddQueryParameter("start", startIndex);

                        pageSize = (remaining > pageSize) ? pageSize : remaining;
                        _request.Parameters.RemoveParameter("rows");
                        _request.AddQueryParameter("rows", pageSize);
                    }
                } while (!maxReached);

                await Task.Delay(pollingDelayInMs, cancellationToken);
            }

            return records;
        }
    }
}