using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Exceptions;
using TFEHelper.Backend.Domain.Repositories;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Common;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Processors.BibTeX;
using TFEHelper.Backend.Services.Processors.CSV;
using TFEHelper.Backend.Tools.ComponentModel;
using TFEHelper.Backend.Tools.Files;
using Publication = TFEHelper.Backend.Domain.Classes.Models.Publication;

namespace TFEHelper.Backend.Services.Implementations.Business
{
    public sealed class PublicationService : IPublicationService
    {
        private readonly ILogger<PublicationService> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public PublicationService(ILogger<PublicationService> logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PublicationDTO entity, CancellationToken cancellationToken = default)
        {
            _repository.Publications.Create(_mapper.Map<Publication>(entity));
            await _repository.SaveAsync(cancellationToken);
        }

        public async Task CreateRangeAsync(IEnumerable<PublicationDTO> entities, CancellationToken cancellationToken = default)
        {
            _repository.Publications.CreateRange(_mapper.Map<IEnumerable<Publication>>(entities));
            await _repository.SaveAsync(cancellationToken);
        }

        public async Task<PublicationDTO> GetAsync(int id, bool tracked = true, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties)
        {
            var publications = await _repository.Publications.GetAsync(
                p => p.Id == id,
                tracked,
                cancellationToken,
                _mapper.Map<IEnumerable<Expression<Func<Publication, object>>>>(navigationProperties).ToArray());

            if (raiseErrorWhenNoResult && !publications.Any()) throw new EntityNotFoundException<Publication>(id);

            return _mapper.Map<PublicationDTO>(publications.First());
        }

        public async Task<IEnumerable<PublicationDTO>> GetListAsync(Expression<Func<PublicationDTO, bool>>? filter = null, bool tracked = true, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties)
        {
            var publications = await _repository.Publications.GetAsync(
                _mapper.Map<Expression<Func<Publication, bool>>>(filter),
                tracked,
                cancellationToken,
                _mapper.Map<IEnumerable<Expression<Func<Publication, object>>>>(navigationProperties).ToArray());

            if (raiseErrorWhenNoResult && !publications.Any()) throw new EntityNotFoundException<Publication>();

            return _mapper.Map<IEnumerable<PublicationDTO>>(publications);
        }

        public async Task<IEnumerable<PublicationDTO>> GetListAsync(SearchSpecificationDTO searchSpecification, bool tracked = true, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties)
        {
            var publications = await _repository.Publications.RunDatabaseQueryAsync(
                searchSpecification.Query,
                _mapper.Map<IEnumerable<SearchParameterDTO>, IEnumerable<DatabaseParameter>>(searchSpecification.Parameters),
                cancellationToken,
                _mapper.Map<IEnumerable<Expression<Func<Publication, object>>>>(navigationProperties).ToArray());

            if (raiseErrorWhenNoResult && !publications.Any()) throw new EntityNotFoundException<Publication>();

            return _mapper.Map<IEnumerable<PublicationDTO>>(publications);
        }

        public async Task<PaginatedListDTO<PublicationDTO>> GetListPaginatedAsync(PaginationParametersDTO parameters, Expression<Func<PublicationDTO, bool>>? filter = null, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties)
        {
            var publications = await _repository.Publications.GetPaginatedAsync(
                _mapper.Map<PaginationParameters>(parameters),
                _mapper.Map<Expression<Func<Publication, bool>>>(filter),
                cancellationToken,
                _mapper.Map<IEnumerable<Expression<Func<Publication, object>>>>(navigationProperties).ToArray());

            if (raiseErrorWhenNoResult && publications.Items.Count == 0) throw new EntityNotFoundException<Publication>();

            return _mapper.Map<PaginatedListDTO<PublicationDTO>>(publications);
        }

        public async Task<PublicationDTO> UpdateAsync(PublicationDTO publication, CancellationToken cancellationToken = default)
        {
            var publications = await _repository.Publications.GetAsync(
                p => p.Id == publication.Id,
                tracked: false,
                cancellationToken);

            if (!publications.Any()) throw new EntityNotFoundException<Publication>(publication.Id);

            var publicationUpdated = _repository.Publications.Update(_mapper.Map<Publication>(publication));
            await _repository.SaveAsync(cancellationToken);

            return _mapper.Map<PublicationDTO>(publicationUpdated);
        }

        public async Task RemoveAsync(PublicationDTO entity, CancellationToken cancellationToken = default)
        {
            var _entity = await _repository.Publications.GetAsync(p => p.Id == entity.Id, tracked: false, cancellationToken: cancellationToken);

            if (!_entity.Any()) throw new EntityNotFoundException<Publication>(entity.Id);

            _repository.Publications.Remove(_mapper.Map<Publication>(entity));

            await _repository.SaveAsync(cancellationToken);
        }

        public async Task ImportPublicationsAsync(string filePath, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            List<Publication> publications = new List<Publication>();
            var _formatType = _mapper.Map<FileFormatType>(formatType);
            var _source = _mapper.Map<SearchSourceType>(source);

            switch (_formatType)
            {
                case FileFormatType.BibTeX:
                    publications = await BibTeXProcessor.ImportAsync(filePath, _source, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    publications = await CSVProcessor.ImportAsync(filePath, _source, cancellationToken);
                    break;
                default:
                    break;
            }

            if (discardInvalidRecords)
            {
                publications.RemoveAll(p => !ModelValidator.IsModelValid(p));
            }

            _repository.Publications.CreateRange(publications);
            await _repository.SaveAsync(cancellationToken);
        }

        public async Task<string> ExportPublicationsAsync(IEnumerable<PublicationDTO> publications, FileFormatDTOType formatType, string? filePath = null, CancellationToken cancellationToken = default)
        {
            var _filePath = filePath ?? Path.Combine(Path.GetTempPath(), FileHelper.GetRandomFileName(_mapper.Map<FileFormatType>(formatType).ToFileNameExtension()));
            var _publications = _mapper.Map<IEnumerable<Publication>>(publications);
            var _formatType = _mapper.Map<FileFormatType>(formatType);

            switch (_formatType)
            {
                case FileFormatType.BibTeX:
                    await BibTeXProcessor.ExportAsync(_publications, _filePath, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    await CSVProcessor.ExportAsync(_publications, _filePath, cancellationToken);
                    break;
                default:
                    break;
            }

            return _filePath;
        }
    }
}
#warning mejorar la forma en que se exporta e importa CSV y BibTeX (ver abajo)
/*
*  La idea es que Export e Import tomen Publication pero que al final del día mapeen hacia CSVRecord y BibTeXRecord ya que cada "formato" tiene sus
*  particularidades (CSV puede tener todos los datos de la base de datos pero BibTeX no).  Para lograr esto, recomiendo que se deje Publication como una
*  entidad pura "modelo" sin decoraciones de BibTeX ya que eso confunde un poco.  Entonces, BibTeXRecord implementaría IBibTexRecord y habría que hacer
*  todo el mapping "desde y hacia" Publication / PublicationDTO...
*  Todo lo anterior hay que hacerlo implementando Strategy para no enmarañar mucho el código de ExportPublicationsAsync.
*/
