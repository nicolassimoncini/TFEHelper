using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);

        void CreateRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties);

        PaginatedList<T> GetPaginated(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] navigationProperties);

        T Update(T publication);

        void Remove(T entity);

        Task<IEnumerable<T>>RunDatabaseQueryAsync(string query, List<IDatabaseParameter>? parameters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties);
    }
}
