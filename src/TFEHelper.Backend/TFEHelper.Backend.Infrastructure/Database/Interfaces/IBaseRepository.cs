using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API.Specifications;


namespace TFEHelper.Backend.Infrastructure.Database.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includedProperties = null, CancellationToken cancellationToken = default);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includedProperties = null, CancellationToken cancellationToken = default);

        PaginatedList<T> GetAllPaginated(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, string? includedProperties = null);

        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
