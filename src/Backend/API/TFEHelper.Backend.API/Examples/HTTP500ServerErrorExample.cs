using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Middleware;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples
{
    public class HTTP500ServerErrorExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                StatusCode = (HttpStatusCode)StatusCodes.Status500InternalServerError,
                ErrorMessages = new List<string> { "Exception details..."},
                IsSuccessful = false,
                Payload = null
            };
        }
    }
}
