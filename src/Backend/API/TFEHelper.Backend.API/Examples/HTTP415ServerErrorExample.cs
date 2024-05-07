using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples
{
    public class HTTP415ServerErrorExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                StatusCode = (HttpStatusCode)StatusCodes.Status415UnsupportedMediaType,
                ErrorMessages = new List<string> { "Unsupported Media Type." },
                IsSuccessful = false,
                Payload = null
            };
        }
    }
}
