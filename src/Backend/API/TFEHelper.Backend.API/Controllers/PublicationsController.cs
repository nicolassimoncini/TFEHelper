using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.API.Examples;
using TFEHelper.Backend.API.Examples.Publications;
using TFEHelper.Backend.API.Filters;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Tools.Files;

namespace TFEHelper.Backend.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
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

        private static string GetContentType(string path)
        {
            return Path.GetExtension(path).ToLowerInvariant() switch
            {
                ".bib" => "application/x-bibtex",
                ".csv" => "text/csv",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }

        /// <summary>
        /// Gets all publications from database.
        /// </summary>
        /// <response code="200">Returns a list of publication objects.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPublicationsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        public async Task<ActionResult<APIResponseDTO>> GetPublications(CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Publications.GetListAsync(cancellationToken:cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        /// <summary>
        /// Gets a list of list of repeated publications based on its titles.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a list of list of repeated publications.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet("Repeated")]
        [ResponseCache(CacheProfileName = "Default30")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPublicationsRepeatedResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        public async Task<ActionResult<APIResponseDTO>> GetPublicationsRepeated(CancellationToken cancellationToken = default)
        {
            IEnumerable<IEnumerable<PublicationDTO>> publications = await _services.Publications.GetListRepeatedAsync(cancellationToken: cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        /// <summary>
        /// Gets all publications from database under pagination schema.
        /// </summary>
        /// <remarks>
        /// This API returns the publications requested wrapped in a structure containing the named publications plus the metadata described as follows:
        /// <ul>
        ///     <li>PageId: The current page number.</li>
        ///     <li>PageSize: The defined page size.</li>
        ///     <li>TotalPages: The calculated total pages count according to the pagination criteria (that is, TotalCount / PageSize).</li>
        ///     <li>TotalCount: The total quantity of records returned from database.</li>
        /// </ul>
        /// </remarks>
        /// <param name="parameters">An object containing the page number to be requested and the page size.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a list of publications.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet("Paginated")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = new[] { "*" })]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(PaginationParametersDTO), typeof(GetPublicationsPaginatedRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPublicationsPaginatedResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> GetPublicationsPaginated([FromQuery] PaginationParametersDTO parameters, CancellationToken cancellationToken = default)
        {
            PaginatedListDTO<PublicationDTO> publications = await _services.Publications.GetListPaginatedAsync(parameters, cancellationToken: cancellationToken);

            _response.IsSuccessful = publications.Items.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        /// <summary>
        /// Gets a publication based on its id.
        /// </summary>
        /// <param name="id">The id that univocally identifies the publication.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a publication.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="404">Returns a not found error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpGet("{id:int}", Name = "GetPublication")]
        [ResponseCache(CacheProfileName = "Default30")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPublicationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(HTTP404ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> GetPublication(int id, CancellationToken cancellationToken = default)
        {
            PublicationDTO publication = await _services.Publications.GetAsync(id, raiseErrorWhenNoResult: true, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.Payload = publication;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        /// <summary>
        /// Gets a list of publications according to provided search parameters.
        /// </summary>
        /// <param name="searchSpecification">
        /// The object containing the search criteria aligned to the following "SQL named parameters" structure:<br/>
        /// <ul>
        ///     <li>Query: ANSI SQL string omitting the entity, where parameters should be prefixed with "@". eg: Title = @Title AND Abstract LIKE @Abstract</li>
        ///     <li>Parameters: A list of key-value pair where Key corresponds to the name of the "field" referenced in the query and value to the desired search value.  eg: { Name = "Title", Value = "%text%" }.</li>
        ///     <li>Narrowings: A list of objects containing the "NEAR" commands for extra filtering the results. A Narrowing object is defined as:
        ///         <ul>
        ///             <li>FieldName: The name of the field in which the narrow search will be applied.</li>
        ///             <li>FirstSentence: The first sentence.</li>
        ///             <li>SecondSentence: The second sentence.</li>
        ///             <li>MinimumDistance: The minimum distance (in words) accepted between FirstSentence and SecondSentence.</li>
        ///         </ul>
        ///         This feature can be interpreted as "return publications for wich the field FieldName contains the sentence FirstSentence and the sentence SecondSentence and the distance between them is lower o equals to MinimumDistance".
        ///     </li>
        /// </ul>
        /// <br/>Notes: 
        /// <ul>
        ///     <li>Any SQL boolean expression can be used.  eg: "(FieldA = @FieldA OR FieldB LIKE @FieldB) AND (FieldC >= @FieldC)".</li>
        ///     <li>Although ordering and grouping is not mandatory in the Query field, it can be included.</li>
        ///     <li>If Narrowing object is not populated, the narrowing process is ignored.</li>
        /// </ul>
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a list of publications.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost("Search")]
        [ResponseCache(CacheProfileName = "Default30", VaryByQueryKeys = new[] { "*" })]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(SearchSpecificationDTO), typeof(SearchPublicationsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SearchPublicationsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> SearchPublications([FromBody] SearchSpecificationDTO searchSpecification, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> publications = await _services.Publications.GetListAsync(searchSpecification, cancellationToken: cancellationToken);

            _response.IsSuccessful = publications.Any();
            _response.Payload = publications;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        /// <summary>
        /// Creates a list of publications.
        /// </summary>
        /// <param name="publications">The list of publications.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a confirmation object with the "Created" http status.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(IEnumerable<PublicationDTO>), typeof(CreatePublicationsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(CreatePublicationsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> CreatePublications([FromBody] IEnumerable<PublicationDTO> publications, CancellationToken cancellationToken = default)
        {
            await _services.Publications.CreateRangeAsync(publications, cancellationToken:cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;

            return Ok(_response);
        }

        /// <summary>
        /// Removes a publication.
        /// </summary>
        /// <param name="id">The id that univocally identifies the publication to be removed.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a publication.</response>
        /// <response code="404">Returns a not found error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpDelete("{id:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RemovePublicationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(HTTP404ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        public async Task<ActionResult<APIResponseDTO>> RemovePublication(int id, CancellationToken cancellationToken = default)
        {
            await _services.Publications.RemoveAsync(new PublicationDTO() { Id = id }, cancellationToken: cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        /// <summary>
        /// Updates a publication.
        /// </summary>
        /// <param name="publication">The publication object to be updated.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the updated publication.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="404">Returns a not found error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(PublicationDTO), typeof(UpdatePublicationRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdatePublicationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(HTTP404ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> UpdatePublication([FromBody] PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            await _services.Publications.UpdateAsync(publication, cancellationToken);
            
            _response.IsSuccessful = true;
            _response.Payload = publication;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        /// <summary>
        /// Performs a patch to a publication.
        /// </summary>
        /// <param name="id">The key that univocally identifies the publication to be updated.</param>
        /// <param name="patch">The patch configuration to be applied to the publication.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the updated publication.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="404">Returns a not found error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPatch("{id:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(Operation), typeof(PatchPublicationRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PatchPublicationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(HTTP404ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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

        /// <summary>
        /// Imports a list of publications from file.
        /// </summary>
        /// <param name="filePath">The full path of the file containing the publications.</param>
        /// <param name="formatType">The format type of the import file.<br/>
        /// The supported format types ids can be retrieved from /api/Configuration/Enumerators (FileFormatDTOType).
        /// </param>
        /// <param name="source">The academic source type from where all records in the file were collected.</param>
        /// <param name="discardInvalidRecords">If true, only wrong formatted recrods will be discarded.  If false, an exception will be thrown when at least one record does not comply with the format.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the import confirmation.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost("Import")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ImportPublicationsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> ImportPublications(string filePath, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)        
        {
            await _services.Publications.ImportPublicationsAsync(filePath, formatType, source, discardInvalidRecords, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;
            
            return Ok(_response);
        }

        /// <summary>
        /// Imports a list of publications from file stream.
        /// </summary>
        /// <param name="file">The HTTP stream reference to the file containing the publication.</param>
        /// <param name="formatType">The format type of the import file.<br/>
        /// The supported format types ids can be retrieved from /api/Configuration/Enumerators (FileFormatDTOType).
        /// </param>
        /// <param name="source">The academic source type from where all records in the file were collected.</param>
        /// <param name="discardInvalidRecords">If true, only wrong formatted recrods will be discarded.  If false, an exception will be thrown when at least one record does not comply with the format.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns a confirmation object with the "Created" http status.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="415">Returns a unsupported media type error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost("ImportAsStream")]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ImportPublicationsAsStreamResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status415UnsupportedMediaType, typeof(HTTP415ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> ImportPublicationsAsStream(IFormFile file, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            var filePath = Path.Combine(Path.GetTempPath(), FileHelper.GetRandomFileName("tmp"));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            await _services.Publications.ImportPublicationsAsync(filePath, formatType, source, discardInvalidRecords, cancellationToken);

            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.Created;

            try { System.IO.File.Delete(filePath); } catch (Exception) { }

            return Ok(_response);
        }

        /// <summary>
        /// Exports a list of publications to a specified file under a specific format.
        /// </summary>
        /// <param name="filePath">The full path of the file that will contain the exported publications.</param>
        /// <param name="formatType">The format type of the exported file.<br/>
        /// The supported format types ids can be retrieved from /api/Configuration/Enumerators (FileFormatDTOType).
        /// </param>
        /// <param name="publications">The list of publications to be exported.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the export confirmation.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost("Export")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(IEnumerable<PublicationDTO>), typeof(ExportPublicationsRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportPublicationsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<APIResponseDTO>> ExportPublications(string filePath, FileFormatDTOType formatType, [FromBody] IEnumerable<PublicationDTO>? publications = null, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> _publications = publications ?? await _services.Publications.GetListAsync();
            await _services.Publications.ExportPublicationsAsync(_publications, formatType, filePath, cancellationToken);

            _response.IsSuccessful = true;
            _response.Payload = filePath;
            _response.StatusCode = HttpStatusCode.OK;
            
            return Ok(_response);
        }

        /// <summary>
        /// Exports a list of publications to a specified file as stream under a specific format.
        /// </summary>
        /// <param name="formatType">The format type of the exported file stream.<br/>
        /// The supported format types ids can be retrieved from /api/Configuration/Enumerators (FileFormatDTOType).
        /// </param>
        /// <param name="publications">The list of publications to be exported.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Returns the file stream as attachment.</response>
        /// <response code="400">Returns a bad request error message.</response>
        /// <response code="500">Returns single error object.</response>
        [HttpPost("ExportAsStream"), DisableRequestSizeLimit]
        //[SwaggerResponse(StatusCodes.Status200OK, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FileStreamResult))]        
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(APIResponseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(APIResponseDTO))]
        [SwaggerRequestExample(typeof(IEnumerable<PublicationDTO>), typeof(ExportPublicationsAsStreamRequestExample))]
        //[SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportPublicationsAsStreamResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(FileStreamResult))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(HTTP400ServerErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(HTTP500ServerErrorExample))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ExportPublicationsAsStream(FileFormatDTOType formatType, [FromBody] IEnumerable<PublicationDTO>? publications = null, CancellationToken cancellationToken = default)
        {
            IEnumerable<PublicationDTO> _publications = publications ?? await _services.Publications.GetListAsync();
            var filePath = await _services.Publications.ExportPublicationsAsync(_publications, formatType, cancellationToken: cancellationToken);

            var memoryStream = new MemoryStream(await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken));
            memoryStream.Position = 0;

            try { System.IO.File.Delete(filePath); } catch (Exception) { }

            return File(memoryStream, GetContentType(filePath), "publications" + Path.GetExtension(filePath));
        }
    }
}