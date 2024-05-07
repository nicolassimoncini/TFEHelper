using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Middleware;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples
{
    public class HTTP400ServerErrorExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                StatusCode = (HttpStatusCode)StatusCodes.Status400BadRequest,
                ErrorMessages = new List<string> { "Validation error details..."},
                IsSuccessful = false,
                Payload = null
            };
        }
    }
}
