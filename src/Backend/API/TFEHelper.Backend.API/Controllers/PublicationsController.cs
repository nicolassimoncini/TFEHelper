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
            PublicationDTO publication = await _services.Publications.GetAsync(id, raiseErrorWhenNoResult: true, cancellationToken: cancellationToken);

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
            await _services.Publications.RemoveAsync(new PublicationDTO() { Id = id }, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDTO>> UpdatePublication([FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            await _services.Publications.UpdateAsync(publication, cancellationToken);
            
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
        public async Task<ActionResult<APIResponseDTO>> PatchPublication(int id, JsonPatchDocument<PublicationDTO> patch, CancellationToken cancellationToken = default)
        {
            PublicationDTO _currentPublication = await _services.Publications.GetAsync(id, tracked: false, raiseErrorWhenNoResult: true, cancellationToken: cancellationToken);

            patch.ApplyTo(_currentPublication, ModelState);

            if (!TryValidateModel(ModelState)) return BadRequest(ModelState);

            await _services.Publications.UpdateAsync(_currentPublication, cancellationToken: cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = _currentPublication;
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