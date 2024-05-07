using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class ExportPublicationsAsStreamResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                ErrorMessages = { },
                IsSuccessful = true,
                Payload = "blob:http://localhost:5000/d275a085-f16a-450c-8078-083d76a1dcf1",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
