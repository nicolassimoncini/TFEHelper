using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Interfaces;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;

namespace TFEHelper.Backend.Core.Engine.Implementations
{
    public class EntitiesManager : IEntitiesManager
    {
        private readonly ILogger<EntitiesManager> _logger;
        private readonly IRepository _repository;

        public EntitiesManager(ILogger<EntitiesManager> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.CreateAsync(entity, cancellationToken);
        }

        public async Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.CreateRangeAsync(entities, cancellationToken);
        }

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>>? filter, string? includedProperties, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            return await _repository.GetAllAsync(filter, includedProperties, cancellationToken);
        }

        public PaginatedList<T> GetAllPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter, string? includedProperties) where T : class, ITFEHelperModel
        {
            return _repository.GetAllPaginated(parameters, filter, includedProperties);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter, bool tracked, string? includedProperties, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            return await _repository.GetAsync(filter, tracked, includedProperties, cancellationToken);
        }

        public async Task RemoveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.RemoveAsync(entity, cancellationToken);
        }

        public async Task<T> UpdateAsync<T>(T publication, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            return await _repository.UpdateAsync(publication, cancellationToken);
        }
    }
}
