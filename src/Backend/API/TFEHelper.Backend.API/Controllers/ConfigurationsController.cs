using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Examples;
using TFEHelper.Backend.API.Examples.Configurations;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly IServiceManager _services;
        private readonly IMapper _mapper;
        protected APIResponseDTO _response;

        public ConfigurationsController(ILogger<PublicationsController> logger, IServiceManager services, IMapper mapper)
        {
            _logger = logger;
            _services = services;
            _mapper = mapper;
            _response = new();
        }

        /// <summary>
        /// Gets a list of configuration objects needed for feeding several API requests parameters.
        /// </summary>        
        /// <remarks>
        /// This API should be used first in order to assign front-end's GUI options that must be used afterwards for several APIs requests parameters.<br/>
        /// Following is the list of enumeration tables returned by this API and their usage:<br/>
        /// 
        /// | Table name | Description | Used by |
        /// |---|---|---|
        /// | BibTeXPublicationDTOType | Most common BibTeX publication types according to BibTeX.org specification. | <li>"Publication type id" field in any Publication object used in /api/Publications.</li>|
        /// | FileFormatDTOType | The file formats currently TFEHelper backend supports for exporting. | <li>Import and export publications in /api/Publications.</li>|
        /// | SearchSourceDTOType | The accepted academic sources. | <li>"Source type id" field in any Publication object used in /api/Publications.</li><br/><li>Fixed "Source type" returned by a "Collector" plugin in /api/Plugins.</li>|
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of named lists of key-value pairs containing all enumeration tables.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet("/api/[controller]/Enumerators")]
        [ResponseCache(CacheProfileName = "Default30")]
        [SwaggerResponse(StatusCodes.Status200OK, type:typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllEnumeratorsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        public ActionResult<APIResponseDTO> GetAllEnumerators()
        {
            IEnumerable<EnumerationTableDTO> enumerations = _services.Configurations.GetEnumerationTables();

            _response.IsSuccessful = enumerations.Any();
            _response.Payload = enumerations;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
    }
}