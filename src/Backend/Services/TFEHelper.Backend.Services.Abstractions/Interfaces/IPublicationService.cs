using System;
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

        Task<PublicationDTO> GetAsync(int id, bool tracked = true, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<IEnumerable<PublicationDTO>> GetListAsync(Expression<Func<PublicationDTO, bool>>? filter = null, bool tracked = true, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<IEnumerable<PublicationDTO>> GetListAsync(SearchSpecificationDTO searchSpecification, bool tracked = true, bool raiseErrorWhenNoResult = false,CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<IEnumerable<IEnumerable<PublicationDTO>>> GetListRepeatedAsync(CancellationToken cancellationToken = default);

        Task<PaginatedListDTO<PublicationDTO>> GetListPaginatedAsync(PaginationParametersDTO parameters, Expression<Func<PublicationDTO, bool>>? filter = null, bool raiseErrorWhenNoResult = false, CancellationToken cancellationToken = default, params Expression<Func<PublicationDTO, object>>[] navigationProperties);

        Task<PublicationDTO> UpdateAsync(PublicationDTO publication, CancellationToken cancellationToken = default);

        Task RemoveAsync(PublicationDTO entity, CancellationToken cancellationToken = default);

        Task ImportPublicationsAsync(string filePath, FileFormatDTOType formatType, SearchSourceDTOType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default);

        Task<string> ExportPublicationsAsync(IEnumerable<PublicationDTO> publications, FileFormatDTOType formatType, string? filePath = null, CancellationToken cancellationToken = default);
    }
}
