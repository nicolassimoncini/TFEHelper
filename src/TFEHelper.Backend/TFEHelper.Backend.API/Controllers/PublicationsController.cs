using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PublicationsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly ITFEHelperOrchestrator _orchestrator;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PublicationsController(ILogger<PublicationsController> logger, ITFEHelperOrchestrator orchestrator, IMapper mapper)
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
        public async Task<ActionResult<APIResponse>> GetPublications(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Obteniendo publicaciones...");

            IEnumerable<Publication> publicationList = await _orchestrator.GetListAsync<Publication>(cancellationToken:cancellationToken);

            _response.IsSuccessful = publicationList.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("Paginated")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = ["parameters"])]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> GetPublicationsPaginated([FromQuery] PaginationParameters parameters)
        {
            var publicationList = _orchestrator.GetListPaginated<Publication>(parameters);

            _response.IsSuccessful = publicationList.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
            _response.StatusCode = HttpStatusCode.OK;
            _response.TotalPages = publicationList.Metadata.TotalPages;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetPublication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetPublication(int id, CancellationToken cancellationToken = default)
        {
            var publication = await _orchestrator.GetAsync<Publication>(v => v.Id == id, cancellationToken: cancellationToken);

            if (publication == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return NotFound(_response);
            }

            _response.IsSuccessful = true;
            _response.Payload = _mapper.Map<PublicationDTO>(publication);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost("Search")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> SearchPublications([FromBody] SearchSpecification specification, CancellationToken cancellationToken = default)
        {
            IEnumerable<Publication> publicationList = await _orchestrator.GetListAsync<Publication>(specification, cancellationToken: cancellationToken);

            _response.IsSuccessful = publicationList.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePublications([FromBody] List<PublicationDTO> publications, CancellationToken cancellationToken = default)
        {
            List<Publication> models = _mapper.Map<List<Publication>>(publications);
            await _orchestrator.CreateRangeAsync(models, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;

            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemovePublication(int id, CancellationToken cancellationToken = default)
        {
            var publication = await _orchestrator.GetAsync<Publication>(v => v.Id == id, cancellationToken: cancellationToken);

            if (publication == null)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            await _orchestrator.RemoveAsync(publication, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.Payload = _mapper.Map<PublicationDTO>(publication);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePublication(int id, [FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            if (publication == null || id != publication.Id)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Payload = publication;
                return BadRequest(_response);
            }

            Publication model = _mapper.Map<Publication>(publication);

            await _orchestrator.UpdateAsync(model, cancellationToken: cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = publication;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePartialPublication(int id, JsonPatchDocument<PublicationDTO> publication, CancellationToken cancellationToken = default)
        {
            var _currentPublication = await _orchestrator.GetAsync<Publication>(v => v.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (_currentPublication == null) return NotFound();

            PublicationDTO _currentPublicationDTO = _mapper.Map<PublicationDTO>(_currentPublication);

            publication.ApplyTo(_currentPublicationDTO, ModelState);

            Publication modelo = _mapper.Map<Publication>(_currentPublicationDTO);

            await _orchestrator.UpdateAsync(modelo, cancellationToken: cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = _currentPublicationDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost("Import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ImportPublications(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            await _orchestrator.ImportPublicationsAsync(filePath, formatType, source, discardInvalidRecords, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;
            return Ok(_response);
        }

        [HttpPost("Export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ExportPublications(string filePath, FileFormatType formatType, [FromBody] List<Publication>? publications = null, CancellationToken cancellationToken = default)
        {
            var pubs = (publications == null) ? await _orchestrator.GetListAsync<Publication>() : publications;
            await _orchestrator.ExportPublicationsAsync(pubs, filePath, formatType, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}