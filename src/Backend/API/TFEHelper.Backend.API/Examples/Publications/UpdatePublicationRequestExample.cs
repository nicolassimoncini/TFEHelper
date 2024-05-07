using Swashbuckle.AspNetCore.Filters;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class UpdatePublicationRequestExample : IExamplesProvider<PublicationDTO>
    {
        public PublicationDTO GetExamples()
        {
            return new PublicationDTO()
            {
                Id = 1,
                Abstract = "The article abstract text 1...",
                Authors = "Author 1, Author 2",
                DOI = "DOI value",
                ISBN = "00-1134875",
                ISSN = "11-80785",
                Key = "ARS-01588",
                Keywords = "keyword example, keyword example",
                Pages = "1-58",
                Source = SearchSourceDTOType.PubMed,
                Title = "The article title",
                Type = BibTeXPublicationDTOType.InBook,
                URL = "",
                Year = 2013
            };
        }
    }
}
