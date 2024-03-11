using RestSharp;
using TFEHelper.Backend.Core.API.SpringerLink.DTO;
using TFEHelper.Backend.Core.API.SpringerLink.Enums;

namespace TFEHelper.Backend.Core.API.SpringerLink.Classes
{
    /// <summary>
    /// Default SpringerLink API consumer.
    /// </summary>
    public class SpringerLinkAPIWrapper : IAPIWrapper, IDisposable
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;
        private List<SpringerLinkRecordDTO> _records = new List<SpringerLinkRecordDTO>();

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

        public void Setup(string query, DateTime? publicationDateFrom, DateTime? publicationDateTo, string? subject, int pageSize = 100)
        {
            string q = query;

            if (publicationDateFrom != null && publicationDateTo != null)
                q = q + " onlinedatefrom:" + publicationDateFrom?.ToString("yyyy-MM-dd") + " onlinedateto:" + publicationDateTo?.ToString("yyyy-MM-dd");

            if (subject != null)
                q = q + " subject:" + '\u0022' + subject + '\u0022';

            _request.AddQueryParameter("q", q);
            _client.AddDefaultParameter("p", pageSize.ToString());
        }


        public async Task<List<SpringerLinkRecordDTO>> GetRecordsAsync(CancellationToken cancellationToken = default)
        {
            SpringerLinkRootDTO _response = await _client.GetAsync<SpringerLinkRootDTO>(_request, cancellationToken) ?? new SpringerLinkRootDTO();
            SpringerLinkResultDTO _result = _response.Result.First();

            _request.Parameters.RemoveParameter("s");
            _request.AddQueryParameter("s", _result.Start + _result.PageLength);

            Console.WriteLine($"Retrieved {_result.Start + _result.PageLength - 1} / {_result.Total}");

            return _response.Records;
        }


        public async Task<List<SpringerLinkRecordDTO>> GetAllRecordsAsync(CancellationToken cancellationToken = default)
        {
            SpringerLinkRootDTO _response;

            do
            {
                _response = await _client.GetAsync<SpringerLinkRootDTO>(_request, cancellationToken) ?? new SpringerLinkRootDTO();
                _records.AddRange(_response.Records);
                SpringerLinkResultDTO _result = _response.Result.First();

                _request.Parameters.RemoveParameter("s");
                _request.AddQueryParameter("s", _result.Start + _result.PageLength);

                Console.WriteLine($"Retrieved {_records.Count()} / {_result.Total}");

            } while (!string.IsNullOrEmpty(_response.NextPage));

            return _records;
        }
    }
}
