﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Exceptions;
using TFEHelper.Backend.Domain.Repositories;
using TFEHelper.Backend.Services.Abstractions.Interfaces;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Processors.BibTeX;
using TFEHelper.Backend.Services.Processors.CSV;
using TFEHelper.Backend.Tools.ComponentModel;
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

        public PaginatedListDTO<PublicationDTO> GetListPaginated(PaginationParametersDTO parameters, Expression<Func<PublicationDTO, bool>>? filter = null, bool raiseErrorWhenNoResult = false, params Expression<Func<PublicationDTO, object>>[] navigationProperties)
        {
            var publications = _repository.Publications.GetPaginated(
                _mapper.Map<PaginationParameters>(parameters),
                _mapper.Map<Expression<Func<Publication, bool>>>(filter),
                _mapper.Map<IEnumerable<Expression<Func<Publication, object>>>>(navigationProperties).ToArray());

            if (raiseErrorWhenNoResult && !publications.Any()) throw new EntityNotFoundException<Publication>();

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

        public async Task ExportPublicationsAsync(IEnumerable<PublicationDTO> publications, string filePath, FileFormatDTOType formatType, CancellationToken cancellationToken = default)
        {
            var _publications = _mapper.Map<IEnumerable<Publication>>(publications);
            var _formatType = _mapper.Map<FileFormatType>(formatType);

            switch (_formatType)
            {
                case FileFormatType.BibTeX:
                    await BibTeXProcessor.ExportAsync(_publications, filePath, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    await CSVProcessor.ExportAsync(_publications, filePath, cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}