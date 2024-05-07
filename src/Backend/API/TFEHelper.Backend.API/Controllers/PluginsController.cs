using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Examples;
using TFEHelper.Backend.API.Examples.Plugins;
using TFEHelper.Backend.API.Filters;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PluginsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly IServiceManager _services;
        private readonly IMapper _mapper;
        protected APIResponseDTO _response;

        public PluginsController(ILogger<PublicationsController> logger, IServiceManager services, IMapper mapper)
        {
            _logger = logger;
            _services = services;
            _mapper = mapper;
            _response = new();
        }

        /// <summary>
        /// Gets a list of available plugins.
        /// </summary>
        /// <remarks>
        /// TFEHelper implements plugin architecture in order to be expanded by allowing third parties to implement extra functionality based on specific restrictions.<br/>
        /// Details on how to implement a plugin in TFEHelper backend can be found at <a href="https://github.com/nicolassimoncini/TFEHelper/tree/main/src/Backend#readme">Github repository</a>.
        /// </remarks>
        /// <response code="200">
        /// Returns a list containing plugin information plus particular execution parameters.<br/>
        /// Meaning of specific fields:
        /// <ul>
        ///     <li>Id: Value that univocally identifies the plugin in the system.  This value must be used, for example, as reference in /api/Plugins/Collectors/{id}/Run.</li>
        ///     <li>Type: The specific type of the plugin.  Note that all plugin types supported can be retrieved from /api/Configurations/Enumerators.</li>
        ///     <li>Parameters: The specific parameters list the plugin supports.  For the Collectors type, it represents the supported "subjects" filters the academic search source supports.</li> 
        /// </ul>
        /// </response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllPluginsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        public ActionResult<APIResponseDTO> GetAllPlugins()
        {
            IEnumerable<PluginInfoDTO> plugins = _services.Plugins.GetAllPlugins();

            _response.IsSuccessful = plugins.Any();
            _response.Payload = plugins;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        /// <summary>
        /// Invokes a specific Collector plugin and returns a list of publications according to provided search parameters.
        /// </summary>
        /// <param name="id">Key used to univocally identify the plugin.</param>
        /// <param name="searchParameters">An object containing the following search configuration attributes:<br/>
        /// <ul>
        ///     <li>Query: The filter to be implemented in the plugin.</li>
        ///     <li>SearchIn: In case the plugin is able to, it will be used to narrow the search to a specific field.</li>
        ///     <li>Subject: The supported "subject" filter code the academic search source supports.  This value must be retrieved from /api/Plugins in the parameters structure related to "Subjects".</li>
        ///     <li>DateFrom: The "date from" filter.</li>
        ///     <li>DateTo: The "date to" filter.</li>
        ///     <li>ReturnQuantityLimit: How many records will the plugin return.</li>
        /// </ul>
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns a list of publications.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>        
        [HttpPost("/api/[controller]/Collectors/{id:int}/Run")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(PublicationsCollectorParametersDTO), typeof(GetPublicationsFromPluginRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPublicationsFromPluginResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> RunPublicationsCollectorPlugin(int id, [FromBody] PublicationsCollectorParametersDTO searchParameters, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Plugins.GetPublicationsFromPluginAsync(id, searchParameters, cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
    }
}