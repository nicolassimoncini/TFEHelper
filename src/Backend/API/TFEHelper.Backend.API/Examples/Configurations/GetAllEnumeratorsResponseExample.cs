using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Controllers;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;

namespace TFEHelper.Backend.API.Examples.Configurations
{
    public class GetAllEnumeratorsResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            return new APIResponseDTO()
            {
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = new(),
                Payload = new List<EnumerationTableDTO>()
                {
                    new EnumerationTableDTO()
                    {
                        Name = "FileFormatDTOType",
                        Items = new List<EnumerationTableItemDTO>()
                        {
                            new EnumerationTableItemDTO(){ Name = "BibTeX", Value = 0 },
                            new EnumerationTableItemDTO(){ Name = "CSV", Value = 1 }
                        }
                    }
                }
            };
        }
    }
}

