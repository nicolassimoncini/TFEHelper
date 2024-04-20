using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponseDTO> GetAllPlugins()
        {
            IEnumerable<PluginInfoDTO> plugins = _services.Plugins.GetAllPlugins();

            _response.IsSuccessful = plugins.Any();
            _response.Payload = plugins;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost("/api/[controller]/Collectors/{id:int}/Run")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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