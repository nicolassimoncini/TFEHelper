using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Classes.Database.Specifications;
using TFEHelper.Backend.Domain.Interfaces;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;

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

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includedProperties = null, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includedProperties != null) 
            {
                foreach (var includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>>? filter = null, string? includedProperties = null, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includedProperties != null) 
            {
                foreach (var includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public PaginatedList<T> GetAllPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, string? includedProperties = null) where T : class, ITFEHelperModel
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includedProperties != null) 
            {
                foreach (var includeProperty in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return PaginatedList<T>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
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

        public async Task<List<T>> RunDatabaseQueryAsync<T>(string query, CancellationToken cancellationToken = default, params IDatabaseParameter[] parameters) where T : class, ITFEHelperModel
        {
            List<DbParameter> _parameters = new();
            parameters.ToList().ForEach(p => _parameters.Add(_dbContext.CreateDbParameter(p)));

            return await _dbContext.Set<T>().FromSqlRaw(query, _parameters.ToArray()).ToListAsync(cancellationToken);
        }
    }
}