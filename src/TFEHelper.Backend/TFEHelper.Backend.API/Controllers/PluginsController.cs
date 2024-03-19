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
        private readonly ITFEHelperEngine _engine;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PluginsController(ILogger<PublicationsController> logger, ITFEHelperEngine engine, IMapper mapper)
        {
            _logger = logger;
            _engine = engine;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> GetAllPlugins()
        {
            IEnumerable<PluginInfo> plugins = _engine.GetAllPlugins();

            _response.IsSuccessful = plugins.Any();
            _response.Payload = _mapper.Map<IEnumerable<PluginInfoDTO>>(plugins);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost("/api/[controller]/Collectors/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RunPublicationsCollectorPlugin(int id, string searchQuery, CancellationToken cancellationToken = default)
        {
            IEnumerable<Publication> publications = await _engine.GetPublicationsFromPluginAsync(id, searchQuery, cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publications);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
    }
}
