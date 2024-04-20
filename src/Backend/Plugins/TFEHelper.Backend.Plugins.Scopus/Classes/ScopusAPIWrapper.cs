using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.Scopus.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TFEHelper.Backend.Plugins.Scopus.Classes
{
    /// <summary>
    /// Default Scopus API consumer.
    /// </summary>
    internal class ScopusAPIWrapper : IDisposable
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;
        private int _returnQuantityLimit;
        private ILogger _logger;

        public ScopusAPIWrapper(string URI, string APIKey, ILogger logger)
        {
            _logger = logger;
            _client = new RestClient(URI);
            _request = new RestRequest();
 
            _client.AddDefaultQueryParameter("apiKey", APIKey);      
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _logger = null;
            _client?.Dispose();
        }

        public void Setup(PublicationsCollectorParametersPLG searchParameters, int pageSize = 25)
        {
            const string queryTAKFormat = "TITLE-ABS-KEY({0})"; // TAK - búsqueda en titulo, abstract y keywords
            _returnQuantityLimit = (searchParameters.ReturnQuantityLimit > 0) ? searchParameters.ReturnQuantityLimit : 0;

            if (pageSize < 0) pageSize = 10;
            if (pageSize > 25) pageSize = 25;

            if (_returnQuantityLimit != 0 && pageSize > _returnQuantityLimit)
                pageSize = _returnQuantityLimit;

            _request.AddQueryParameter("httpAccept", "application/json");
            _request.AddQueryParameter("count", pageSize);
            _request.AddQueryParameter("start", 0);
            _request.AddQueryParameter("query", string.Format(queryTAKFormat, searchParameters.Query));
            _request.AddQueryParameter("date", string.Format("{0}-{1}", searchParameters.DateFrom.Year, searchParameters.DateTo.Year));
        }

        public async Task<List<ScopusEntryDTO>> GetAllRecordsAsync(CancellationToken cancellationToken = default)
        {
            List<ScopusEntryDTO> records = new List<ScopusEntryDTO>();
            ScopusRootDTO response;
            ScopusSearchResultsDTO result;
            bool maxReached = false;
            int remaining;

            _logger.LogInformation("Requesting publications...");

            do
            {
                response = await _client.GetAsync<ScopusRootDTO>(_request, cancellationToken) ?? new ScopusRootDTO();
                records.AddRange(response.Result.Entries);
                result = response.Result;

                _logger.LogInformation($"Retrieved {records.Count()} / {result.TotalResults} records.");

                remaining = _returnQuantityLimit - records.Count();

                if (remaining > 0)
                {
                    _request.Parameters.RemoveParameter("start");
                    _request.AddQueryParameter("start", result.StartIndex +1);

                    if (remaining < result.ItemsPerPage)
                    {
                        _request.Parameters.RemoveParameter("count");
                        _request.AddQueryParameter("count", remaining);
                    }
                }
                else maxReached = true;

            } while (!maxReached && (Math.Round((decimal)(result.TotalResults / result.ItemsPerPage)) >= result.StartIndex +1));

            return records;
        }
    }
}
