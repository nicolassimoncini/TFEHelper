﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        /// <summary>
        /// Creates an APIConsumer.
        /// </summary>
        /// <param name="URI">The API Uri.</param>
        /// <param name="APIKey">The API Key for using as X-Authorization header validation.</param>
        /// <param name="authorizationType">The authorization type</param>
        public SpringerLinkAPIWrapper(string URI, string APIKey, SpringerLinkAPIAuthorizationType authorizationType)
        {
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

        public void Setup(string query, DateOnly publicationDateFrom, DateOnly publicationDateTo, string subject, int pageSize = 100, int returnQuantityLimit = 0)
        {
            _returnQuantityLimit = (returnQuantityLimit > 0) ? returnQuantityLimit : 0;

            string q = query; // [TODO] dar formato de acuerdo a https://dev.springernature.com/adding-constraints
            q = q + " onlinedatefrom:" + publicationDateFrom.ToString("yyyy-MM-dd") + " onlinedateto:" + publicationDateTo.ToString("yyyy-MM-dd");

            if (subject != string.Empty)
                q = q + " subject:" + '\u0022' + subject + '\u0022';

            if (pageSize < 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            if (_returnQuantityLimit != 0 && pageSize > _returnQuantityLimit)
                pageSize = _returnQuantityLimit;

            _request.AddQueryParameter("q", q);
            _request.AddQueryParameter("p", pageSize.ToString());
            //_client.AddDefaultParameter("p", pageSize.ToString());
        }

        public async Task<List<SpringerLinkRecordDTO>> GetAllRecordsAsync(CancellationToken cancellationToken = default)
        {
            List<SpringerLinkRecordDTO> records = new List<SpringerLinkRecordDTO>();
            SpringerLinkRootDTO response;
            bool maxReached = false;
            int remaining;

            do
            {
                response = await _client.GetAsync<SpringerLinkRootDTO>(_request, cancellationToken) ?? new SpringerLinkRootDTO();
                records.AddRange(response.Records);
                SpringerLinkResultDTO result = response.Result.First();

                Console.WriteLine($"Retrieved {records.Count()} / {result.Total}");

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
