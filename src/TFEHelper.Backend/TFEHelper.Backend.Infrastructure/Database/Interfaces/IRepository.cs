using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Interfaces;


namespace TFEHelper.Backend.Infrastructure.Database.Interfaces
{
    public interface IRepository
    {
        Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includedProperties = null, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>>? filter = null, string? includedProperties = null, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        PaginatedList<T> GetAllPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, string? includedProperties = null) where T : class, ITFEHelperModel;

        Task<T> UpdateAsync<T>(T publication, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task RemoveAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task SaveAsync<T>(CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;
    }
}
