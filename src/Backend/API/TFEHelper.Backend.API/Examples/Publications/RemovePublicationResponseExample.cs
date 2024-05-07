using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class RemovePublicationResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                ErrorMessages = {},
                IsSuccessful = true,
                Payload = null,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
