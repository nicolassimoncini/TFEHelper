using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class GetPublicationsPaginatedResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = new(),
                Payload = new PaginatedListDTO<PublicationDTO>()
                {
                    Metadata = new MetadataDTO()
                    {
                        PageSize = 2,
                        TotalCount = 100,
                        TotalPages = 50,
                        PageId = 1
                    },
                    Items = new List<PublicationDTO>()
                    {
                        new PublicationDTO()
                        {
                            Abstract = "The article abstract text...",
                            Authors = "Author 1, Author 2",
                            DOI = "DOI value",
                            Id = 1,
                            ISBN = "00-1234455",
                            ISSN = "11-88995",
                            Key = "ARS-44588",
                            Keywords = "keyword example, keyword example",
                            Pages = "1-58",
                            Source = SearchSourceDTOType.Manual,
                            Title = "The article title",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2021
                        },
                        new PublicationDTO()
                        {
                            Abstract = "The article abstract text...",
                            Authors = "Author 3, Author 4",
                            DOI = "DOI value 2",
                            Id = 2,
                            ISBN = "01-1244755",
                            ISSN = "410-85992",
                            Key = "ARS-44721",
                            Keywords = "keyword example, keyword example",
                            Pages = "7-23",
                            Source = SearchSourceDTOType.Manual,
                            Title = "The article title",
                            Type = BibTeXPublicationDTOType.Article,
                            URL = "",
                            Year = 2017
                        }
                    }
                }
            };
        }
    }
}
