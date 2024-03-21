using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PluginsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly ITFEHelperOrchestrator _orchestrator;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PluginsController(ILogger<PublicationsController> logger, ITFEHelperOrchestrator orchestrator, IMapper mapper)
        {
            _logger = logger;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> GetAllPlugins()
        {
            IEnumerable<PluginInfo> plugins = _orchestrator.GetAllPlugins();

            _response.IsSuccessful = plugins.Any();
            _response.Payload = plugins;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost("/api/[controller]/Collectors/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RunPublicationsCollectorPlugin(int id, [FromBody] SearchParameters searchParameters, CancellationToken cancellationToken = default)
        {
            IEnumerable<Publication> publications = await _orchestrator.GetPublicationsFromPluginAsync(id, searchParameters, cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publications);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
    }
}

#warning meter un mecanismo para que la API pueda pedir de forma genérica los datos paramétricos de los plugins (ejemplo SpringerLinkSubjectType y SpringerLinkSearchInElementType
