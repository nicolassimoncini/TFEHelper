using Swashbuckle.AspNetCore.Filters;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class SearchPublicationsRequestExample : IExamplesProvider<SearchSpecificationDTO>
    {
        public SearchSpecificationDTO GetExamples()
        {
            return new SearchSpecificationDTO()
            {
                Query = "Title LIKE @Title AND Year >= @Year",
                Parameters = new List<SearchParameterDTO>() 
                { 
                    new SearchParameterDTO(){ Name = "Title", Value = "%title%" },
                    new SearchParameterDTO(){ Name = "Year", Value = 2021 }
                }
            };
        }
    }
}
