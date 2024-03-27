using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database.Specifications;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class Repository : IRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await SaveAsync<T>(cancellationToken);
        }

        public async Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            await SaveAsync<T>(cancellationToken);
        }

        public async Task SaveAsync<T>(CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            IQueryable<T> result = _dbContext.Set<T>();

            if (!tracked) result = result.AsNoTracking();
            if (filter != null) result = result.Where(filter);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            IQueryable<T> result = _dbContext.Set<T>();

            if (filter != null) result = result.Where(filter);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return await result.ToListAsync(cancellationToken);
        }

        public PaginatedList<T> GetListPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            IQueryable<T> result = _dbContext.Set<T>();

            if (filter != null) result = result.Where(filter);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return PaginatedList<T>.ToPagedList(result, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            _dbContext.Set<T>().Update(entity);
            await SaveAsync<T>(cancellationToken);
            return entity;
        }

        public async Task RemoveAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveAsync<T>(cancellationToken);
        }

        public async Task<List<T>> RunDatabaseQueryAsync<T>(string query, List<IDatabaseParameter>? parameters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            List<DbParameter> _parameters = new();
            parameters?.ForEach(p => _parameters.Add(_dbContext.CreateDbParameter(p)));

            IQueryable<T> result = _dbContext.Set<T>().FromSqlRaw(query, _parameters.ToArray());

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                result = result.Include(navigationProperty);

            return await result.ToListAsync(cancellationToken);
        }
    }
}