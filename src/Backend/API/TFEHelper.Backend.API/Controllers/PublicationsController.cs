using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PublicationsController : ControllerBase
    {
        private readonly ILogger<PublicationsController> _logger;
        private readonly IServiceManager _services;
        private readonly IMapper _mapper;
        protected APIResponseDTO _response;

        public PublicationsController(ILogger<PublicationsController> logger, IServiceManager services, IMapper mapper)
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
        public async Task<ActionResult<APIResponseDTO>> GetPublications(CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Publications.GetListAsync(cancellationToken:cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("Paginated")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = ["parameters"])]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponseDTO> GetPublicationsPaginated([FromQuery] PaginationParametersDTO parameters)
        {
            PaginatedListDTO<PublicationDTO> publications = _services.Publications.GetListPaginated(parameters);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;
            _response.TotalPages = publications.Metadata.TotalPages;
            
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetPublication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> GetPublication(int id, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publication = await _services.Publications.GetListAsync(v => v.Id == id, cancellationToken: cancellationToken);

            if (!publication.Any())
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccessful = false;
                return NotFound(_response);
            }

            _response.IsSuccessful = true;
            _response.Payload = publication;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost("Search")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> SearchPublications([FromBody] SearchSpecificationDTO searchSpecification, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Publications.GetListAsync(searchSpecification, cancellationToken: cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> CreatePublications([FromBody] IEnumerable<PublicationDTO> publications, CancellationToken cancellationToken = default)
        {
            await _services.Publications.CreateRangeAsync(publications, cancellationToken:cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;

            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> RemovePublication(int id, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Publications.GetListAsync(v => v.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (!publications.Any())
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            await _services.Publications.RemoveAsync(publications.First(), cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.Payload = publications.First();
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> UpdatePublication(int id, [FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            if (publication == null || id != publication.Id)
            {
                _response.IsSuccessful = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Payload = publication;
                return BadRequest(_response);
            }

            await _services.Publications.UpdateAsync(publication, cancellationToken: cancellationToken);
            
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
        public async Task<ActionResult<APIResponseDTO>> UpdatePartialPublication(int id, JsonPatchDocument<PublicationDTO> publication, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> _currentPublications = await _services.Publications.GetListAsync(v => v.Id == id, tracked: false, cancellationToken: cancellationToken);

            if (!_currentPublications.Any()) return NotFound();

            publication.ApplyTo(_currentPublications.First(), ModelState);

            await _services.Publications.UpdateAsync(_currentPublications.First(), cancellationToken: cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = _currentPublications.First();
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        [HttpPost("Import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> ImportPublications(string filePath, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)        
        {
            await _services.Publications.ImportPublicationsAsync(filePath, formatType, source, discardInvalidRecords, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;
            
            return Ok(_response);
        }

        [HttpPost("Export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> ExportPublications(string filePath, FileFormatDTOType formatType, [FromBody] IEnumerable<PublicationDTO>? publications = null, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> _publications = (publications == null) ? await _services.Publications.GetListAsync() : publications;
            await _services.Publications.ExportPublicationsAsync(_publications, filePath, formatType, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }
    }
}
#warning meter la "lógica de negocio" dentro de Core (servicio) y encapsular errores con excepciones capturables y convertibles en el middleware...