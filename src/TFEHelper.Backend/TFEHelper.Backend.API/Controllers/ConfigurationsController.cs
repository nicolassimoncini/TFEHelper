using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly ITFEHelperOrchestrator _orchestrator;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public ConfigurationsController(ILogger<PublicationsController> logger, ITFEHelperOrchestrator orchestrator, IMapper mapper)
        {
            _logger = logger;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("/api/[controller]/Enumerators")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> GetAllEnumerators()
        {
            IEnumerable<EnumerationTable> enumerations = _orchestrator.GetEnumerationTables();

            _response.IsSuccessful = enumerations.Any();
            _response.Payload = enumerations;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
    }
}
