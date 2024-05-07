using Swashbuckle.AspNetCore.Filters;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class GetPublicationsPaginatedRequestExample : IExamplesProvider<PaginationParametersDTO>
    {
        public PaginationParametersDTO GetExamples()
        {
            return new PaginationParametersDTO()
            {
                PageNumber = 1,
                PageSize = 30
            };
        }
    }
}
