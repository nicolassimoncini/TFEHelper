using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Repositories;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, ITFEHelperModel
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> result = _dbContext.Set<T>();

            if (!tracked) result = result.AsNoTracking();
            if (filter != null) result = result.Where(filter);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return await result.ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> result = _dbContext.Set<T>();

            if (filter != null) result = result.Where(filter);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return PaginatedList<T>.ToPagedList(await result.ToListAsync(cancellationToken), parameters.PageNumber, parameters.PageSize);
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        public void Remove(T entity) 
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> RunDatabaseQueryAsync(string query, IEnumerable<IDatabaseParameter>? parameters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties)
        {
            const string FMT_QUERY = "SELECT * FROM {0} WHERE {1}";
            var fmtQuery = string.Format(FMT_QUERY, _dbContext.Model.FindEntityType(typeof(T))!.GetSchemaQualifiedTableName(), query);

            List<DbParameter> _parameters = new();
            parameters?.ToList().ForEach(p => _parameters.Add(_dbContext.CreateDbParameter(p)));

            IQueryable<T> result = _dbContext.Set<T>().FromSqlRaw(fmtQuery, _parameters.ToArray());

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return await result.ToListAsync(cancellationToken);
        }
    }
}
