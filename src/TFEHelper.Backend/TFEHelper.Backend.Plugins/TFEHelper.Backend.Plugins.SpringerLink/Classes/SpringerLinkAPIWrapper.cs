using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.SpringerLink.DTO;
using TFEHelper.Backend.Plugins.SpringerLink.Enums;

namespace TFEHelper.Backend.Plugins.SpringerLink.Classes
{
    /// <summary>
    /// Default SpringerLink API consumer.
    /// </summary>
    internal class SpringerLinkAPIWrapper : IDisposable
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;
        private int _returnQuantityLimit;
        private ILogger _logger;

        /// <summary>
        /// Creates an APIConsumer.
        /// </summary>
        /// <param name="URI">The API Uri.</param>
        /// <param name="APIKey">The API Key for using as X-Authorization header validation.</param>
        /// <param name="authorizationType">The authorization type</param>
        public SpringerLinkAPIWrapper(string URI, string APIKey, SpringerLinkAPIAuthorizationType authorizationType, ILogger logger)
        {
            _logger = logger;
            _client = new RestClient(URI);
            _request = new RestRequest("json");

            switch (authorizationType)
            {
                case SpringerLinkAPIAuthorizationType.QueryParameter:
                    {
                        _client.AddDefaultQueryParameter("api_key", APIKey);
                        break;
                    }
                case SpringerLinkAPIAuthorizationType.XAuthorization:
                    {
                        _request.AddHeader("X-Authorization", APIKey);
                        break;
                    }
                default: break;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _client?.Dispose();
        }

        public void Setup(PublicationsCollectorParameters searchParameters, int pageSize = 100)
        {
            _returnQuantityLimit = (searchParameters.ReturnQuantityLimit > 0) ? searchParameters.ReturnQuantityLimit : 0;

            string query = ParseQuery(searchParameters);

            if (pageSize < 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            if (_returnQuantityLimit != 0 && pageSize > _returnQuantityLimit)
                pageSize = _returnQuantityLimit;

            _request.AddQueryParameter("q", query);
            _request.AddQueryParameter("p", pageSize.ToString());
        }

        private string ParseQuery(PublicationsCollectorParameters searchParameters)
        {
            string query = "";

            if (searchParameters.SearchIn != string.Empty)
                query += searchParameters.SearchIn + ":";

            query += searchParameters.Query;
            query += " onlinedatefrom:" + searchParameters.DateFrom.ToString("yyyy-MM-dd") + " onlinedateto:" + searchParameters.DateTo.ToString("yyyy-MM-dd");

            if (searchParameters.Subject != string.Empty)
                query += " subject:" + '\u0022' + searchParameters.Subject + '\u0022';

            return query;
        }

        public async Task<List<SpringerLinkRecordDTO>> GetAllRecordsAsync(CancellationToken cancellationToken = default)
        {
            List<SpringerLinkRecordDTO> records = new List<SpringerLinkRecordDTO>();
            SpringerLinkRootDTO response;
            bool maxReached = false;
            int remaining;

            _logger.LogInformation("Requesting publications...");

            do
            {
                response = await _client.GetAsync<SpringerLinkRootDTO>(_request, cancellationToken) ?? new SpringerLinkRootDTO();
                records.AddRange(response.Records);
                SpringerLinkResultDTO result = response.Result.First();

                _logger.LogInformation($"Retrieved {records.Count()} / {result.Total} records.");

                remaining = _returnQuantityLimit - records.Count();
                
                if (remaining > 0)
                {
                    _request.Parameters.RemoveParameter("s");
                    _request.AddQueryParameter("s", result.Start + result.PageLength);

                    if (remaining < result.PageLength) 
                    {
                        _request.Parameters.RemoveParameter("p");
                        _request.AddQueryParameter("p", remaining);
                    }
                } else maxReached = true;

            } while (!maxReached && !string.IsNullOrEmpty(response.NextPage));

            return records;
        }
    }
}
