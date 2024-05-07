using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class GetPublicationsRepeatedResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = new(),
                Payload = new List<List<PublicationDTO>>()
                {
                    new List<PublicationDTO>()
                    {
                        new PublicationDTO()
                        {
                            Abstract = "Article A",
                            Authors = "Author 1, Author 2",
                            DOI = "DOI value",
                            Id = 0,
                            ISBN = "00-1234455",
                            ISSN = "11-88995",
                            Key = "ARS-44588",
                            Keywords = "keyword example, keyword example",
                            Pages = "1-58",
                            Source = SearchSourceDTOType.Manual,
                            Title = "Article A",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2021
                        },
                        new PublicationDTO()
                        {
                            Abstract = "Article A",
                            Authors = "Author 1, Author 2",
                            DOI = "DOI value",
                            Id = 0,
                            ISBN = "00-1234455",
                            ISSN = "11-88995",
                            Key = "ARS-44588",
                            Keywords = "keyword example, keyword example",
                            Pages = "1-58",
                            Source = SearchSourceDTOType.Manual,
                            Title = "Article A",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2021
                        }
                    },
                    new List<PublicationDTO>()
                    {
                        new PublicationDTO()
                        {
                            Abstract = "Article B",
                            Authors = "Author 1, Author 2",
                            DOI = "DOI value",
                            Id = 0,
                            ISBN = "00-1234455",
                            ISSN = "11-88995",
                            Key = "ARS-44588",
                            Keywords = "keyword example, keyword example",
                            Pages = "1-58",
                            Source = SearchSourceDTOType.Manual,
                            Title = "Article B",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2021
                        },
                        new PublicationDTO()
                        {
                            Abstract = "Article B",
                            Authors = "Author 1, Author 2",
                            DOI = "DOI value",
                            Id = 0,
                            ISBN = "00-1234455",
                            ISSN = "11-88995",
                            Key = "ARS-44588",
                            Keywords = "keyword example, keyword example",
                            Pages = "1-58",
                            Source = SearchSourceDTOType.Manual,
                            Title = "Article B",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2021
                        }
                    }
                }
            };
        }
    }
}
