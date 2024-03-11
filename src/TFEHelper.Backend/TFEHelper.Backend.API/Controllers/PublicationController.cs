using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        private readonly IRepository<Publication> _repository;
        private readonly ITFEEngine _engine;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PublicationController(ILogger<PublicationController> logger, IRepository<Publication> repository, ITFEEngine engine, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _engine = engine;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("GetPublications")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPublications(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Obteniendo publicaciones...");

                IEnumerable<Publication> publicationList = await _repository.GetAllAsync(cancellationToken:cancellationToken);

                _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex, "GetPublications raised an error!");
            }
            return _response;
        }

        [HttpGet("GetPublicationsPaginated")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = ["parameters"])]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetPublicationsPaginated([FromQuery] PaginationParameters parameters)
        {
            try
            {
                var publicationList = _repository.GetAllPaginated(parameters);
                _response.Payload = _mapper.Map<IEnumerable<PublicationDTO>>(publicationList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.TotalPages = publicationList.Metadata.TotalPages;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetPublication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPublication(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError($"Error al obtener publicación con Id = {id}");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }

                var publication = await _repository.GetAsync(v => v.Id == id, cancellationToken: cancellationToken);

                if (publication == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }

                _response.Payload = _mapper.Map<PublicationDTO>(publication);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return _response;
        }

        [HttpPost("CreatePublication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePublication([FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _repository.GetAsync(v => v.Id == publication.Id, cancellationToken: cancellationToken) != null)
                {
                    ModelState.AddModelError("ErrorMessages", $"La publicación con el Id {publication.Id} ya existe!");
                    return BadRequest(ModelState);
                }

                if (publication == null)
                {
                    return BadRequest(publication);
                }

                Publication model = _mapper.Map<Publication>(publication);

                await _repository.CreateAsync(model, cancellationToken: cancellationToken);
                _response.Payload = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetPublication", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemovePublication(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var publication = await _repository.GetAsync(v => v.Id == id, cancellationToken: cancellationToken);
                if (publication == null)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _repository.RemoveAsync(publication, cancellationToken: cancellationToken);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePublication(int id, [FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            try
            {
                if (publication == null || id != publication.Id)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _repository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken) == null)
                {
                    _response.IsSuccessful = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Publication model = _mapper.Map<Publication>(publication);

                await _repository.UpdateAsync(model, cancellationToken: cancellationToken);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return BadRequest(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPublication(int id, JsonPatchDocument<PublicationDTO> publication, CancellationToken cancellationToken = default)
        {
            try
            {
                if (publication == null || id == 0)
                {
                    return BadRequest();
                }

                var _currentPublication = await _repository.GetAsync(v => v.Id == id, tracked: false, cancellationToken: cancellationToken);
                if (_currentPublication == null) return BadRequest();

                PublicationDTO _currentPublicationDTO = _mapper.Map<PublicationDTO>(_currentPublication);

                publication.ApplyTo(_currentPublicationDTO/*, ModelState*/);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Publication modelo = _mapper.Map<Publication>(_currentPublicationDTO);

                await _repository.UpdateAsync(modelo, cancellationToken: cancellationToken);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return BadRequest(_response);
        }

        [HttpPost("ImportPublications")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ImportPublications(string filePath, FileFormatType formatType, SearchSourceType source, CancellationToken cancellationToken = default)
        {
            try
            {
                await _engine.ImportPublicationsAsync(filePath, formatType, source, cancellationToken);

                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return _response;
        }

        [HttpPost("ExportPublications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> ExportPublications(string filePath, FileFormatType formatType, CancellationToken cancellationToken = default)
        {
            try
            {
                await _engine.ExportPublicationsAsync(await _repository.GetAllAsync(), filePath, formatType, cancellationToken);

                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add(ex.ToString());
                _logger.LogError(ex.ToString());
            }
            return _response;
        }
    }
}