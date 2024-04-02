﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Services.Contracts.DTO.API;

namespace TFEHelper.Backend.Services.Abstractions.Interfaces
{
    public interface IPublicationService
    {
        Task CreateAsync(PublicationDTO entity, CancellationToken cancellationToken = default);

        Task CreateRangeAsync(IEnumerable<PublicationDTO> entities, CancellationToken cancellationToken = default);

        Task<List<PublicationDTO>> GetListAsync(Expression<Func<PublicationDTO, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<List<PublicationDTO>> GetListAsync(SearchSpecificationDTO searchSpecification, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        PaginatedListDTO<PublicationDTO> GetListPaginated(PaginationParametersDTO parameters, Expression<Func<PublicationDTO, bool>>? filter = null, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<PublicationDTO> UpdateAsync(PublicationDTO publication, CancellationToken cancellationToken = default);

        Task RemoveAsync(PublicationDTO entity, CancellationToken cancellationToken = default);

        Task ImportPublicationsAsync(string filePath, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default);

        Task ExportPublicationsAsync(List<PublicationDTO> publications, string filePath, FileFormatDTOType formatType, CancellationToken cancellationToken = default);
    }
}
