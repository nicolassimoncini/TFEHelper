using System.Linq.Expressions;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Interfaces;

namespace TFEHelper.Backend.Core.Engine.Interfaces
{
    public interface ITFEHelperOrchestrator
    {
        Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        Task<List<T>> GetListAsync<T>(string filter, string? includedProperties = null, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        PaginatedList<T> GetListPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        Task<T> UpdateAsync<T>(T publication, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task RemoveAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task ImportPublicationsAsync(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default);

        Task ExportPublicationsAsync(List<Publication> publications, string filePath, FileFormatType formatType, CancellationToken cancellationToken = default);

        IEnumerable<PluginInfo> GetAllPlugins();

        IEnumerable<PluginInfo> GetPublicationsCollectorPlugins();

        Task<IEnumerable<Publication>> GetPublicationsFromPluginAsync(int pluginId, SearchParameters searchParameters, CancellationToken cancellationToken = default);

        IEnumerable<EnumerationTable> GetEnumerationTables();
    }
}
