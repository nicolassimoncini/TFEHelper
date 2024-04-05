using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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

        [HttpGet("/api/[controller]/Enumerators")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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