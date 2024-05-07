using Swashbuckle.AspNetCore.Filters;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class CreatePublicationsRequestExample : IExamplesProvider<IEnumerable<PublicationDTO>>
    {
        public IEnumerable<PublicationDTO> GetExamples()
        {
            return new List<PublicationDTO>()
            {
                new PublicationDTO()
                {
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
                },
                new PublicationDTO()
                {
                    Abstract = "The article abstract text 2...",
                    Authors = "Author 1, Author 2",
                    DOI = "DOI value",
                    ISBN = "00-1234455",
                    ISSN = "11-8845",
                    Key = "ARS-477888",
                    Keywords = "keyword example, keyword example",
                    Pages = "1-58",
                    Source = SearchSourceDTOType.Manual,
                    Title = "The article title",
                    Type = BibTeXPublicationDTOType.Article,
                    URL = "",
                    Year = 2018
                }                
            };
        }
    }
}
