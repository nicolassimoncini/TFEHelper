using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples
{
    public class HTTP404ServerErrorExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                StatusCode = (HttpStatusCode)StatusCodes.Status404NotFound,
                ErrorMessages = new List<string> { "The required Entity with id = 1 was not found..." },
                IsSuccessful = false,
                Payload = null
            };
        }
    }
}
