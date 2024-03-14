using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Core.Engine.Implementations;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly ILogger<PublicationController> _logger;
        private readonly ITFEHelperEngine _engine;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PublicationController(ILogger<PublicationController> logger, ITFEHelperEngine engine, IMapper mapper)
        {
            _logger = logger;
            _engine = engine;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("GetPublications")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetPublications(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Obteniendo publicaciones...");

            IEnumerable<Publication> publicationList = await _engine.GetAllAsync<Publication>(cancellationToken:cancellationToken);

            _response.IsSuccessful = publicationList.Any();
            _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("GetPublicationsPaginated")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = ["parameters"])]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> GetPublicationsPaginated([FromQuery] PaginationParameters parameters)
        {
            var publicationList = _engine.GetAllPaginated<Publication>(parameters);

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
            var publication = await _engine.GetAsync<Publication>(v => v.Id == id, cancellationToken: cancellationToken);

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

        [HttpPost("CreatePublication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePublication([FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Publication model = _mapper.Map<Publication>(publication);
            await _engine.CreateAsync(model, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.Payload = publication;
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetPublication", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemovePublication(int id, CancellationToken cancellationToken = default)
        {
            var publication = await _engine.GetAsync<Publication>(v => v.Id == id, cancellationToken: cancellationToken);

            if (publication == null)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            await _engine.RemoveAsync(publication, cancellationToken: cancellationToken);

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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Publication model = _mapper.Map<Publication>(publication);

            await _engine.UpdateAsync(model, cancellationToken: cancellationToken);
            
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
            var _currentPublication = await _engine.GetAsync<Publication>(v => v.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (_currentPublication == null) return NotFound();

            PublicationDTO _currentPublicationDTO = _mapper.Map<PublicationDTO>(_currentPublication);

            publication.ApplyTo(_currentPublicationDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Publication modelo = _mapper.Map<Publication>(_currentPublicationDTO);

            await _engine.UpdateAsync(modelo, cancellationToken: cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = _currentPublicationDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost("ImportPublications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ImportPublications(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            await _engine.ImportPublicationsAsync(filePath, formatType, source, discardInvalidRecords, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;
            return Ok(_response);
        }

        [HttpPost("ExportPublications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ExportPublications(string filePath, FileFormatType formatType, CancellationToken cancellationToken = default)
        {

            await _engine.ExportPublicationsAsync(await _engine.GetAllAsync<Publication>(), filePath, formatType, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}