using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TFEHelper.Backend.Core.Configuration.Interfaces;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Core.Plugin.Interfaces;
using TFEHelper.Backend.Core.Processors.BibTeX;
using TFEHelper.Backend.Core.Processors.CSV;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Classes.Plugin;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Extensions;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using ModelValidator = TFEHelper.Backend.Tools.ComponentModel.ModelValidator;
using SearchParametersFromModel = TFEHelper.Backend.Domain.Classes.Plugin.PublicationsCollectorParameters;
using SearchParametersFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.PublicationsCollectorParameters;

namespace TFEHelper.Backend.Core.Engine.Implementations
{
    public sealed class TFEHelperOrchestrator : ITFEHelperOrchestrator
    {
        private readonly ILogger<TFEHelperOrchestrator> _logger;
        private readonly IRepository _repository;
        private readonly IPluginManager _pluginManager;
        private readonly ITFEHelperConfigurationManager _configurationManager;
        private readonly IMapper _mapper;
        private readonly BibTeXProcessor _bibTeXProcessor;
        private readonly CSVProcessor _csvProcessor;

        public TFEHelperOrchestrator(ILogger<TFEHelperOrchestrator> logger, IRepository repository, IPluginManager pluginManager, ITFEHelperConfigurationManager configurationManager, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _pluginManager = pluginManager;
            _configurationManager = configurationManager;
            _mapper = mapper;
            _bibTeXProcessor = new BibTeXProcessor();
            _csvProcessor = new CSVProcessor();            
        }

        #region DataAccess

        public async Task CreateAsync<T>(T entity, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.CreateAsync(entity, cancellationToken);
        }

        public async Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.CreateRangeAsync(entities, cancellationToken);
        }

        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            return await _repository.GetListAsync(filter, cancellationToken, navigationProperties);
        }

        public async Task<List<T>> GetListAsync<T>(SearchSpecification searchSpecification, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            return await _repository.RunDatabaseQueryAsync<T>(
                searchSpecification.Query,
                _mapper.Map<List<SearchParameter>, List<IDatabaseParameter>>(searchSpecification.Parameters), 
                cancellationToken, 
                navigationProperties);
        }

        public PaginatedList<T> GetListPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            return _repository.GetListPaginated(parameters, filter, navigationProperties);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel
        {
            return await _repository.GetAsync(filter, tracked, cancellationToken, navigationProperties);
        }

        public async Task RemoveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            await _repository.RemoveAsync(entity, cancellationToken);
        }

        public async Task<T> UpdateAsync<T>(T publication, CancellationToken cancellationToken) where T : class, ITFEHelperModel
        {
            return await _repository.UpdateAsync(publication, cancellationToken);
        }

        public async Task ImportPublicationsAsync(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            List<Publication> publications = new List<Publication>();

            switch (formatType)
            {
                case FileFormatType.BibTeX:
                    publications = await _bibTeXProcessor.ImportAsync(filePath, source, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    publications = await _csvProcessor.ImportAsync(filePath, source, cancellationToken);
                    break;
                default:
                    break;
            }

            if (discardInvalidRecords)
            {
                publications.RemoveAll(p => !ModelValidator.IsModelValid(p));
            }

            await CreateRangeAsync(publications, cancellationToken);
        }

        public async Task ExportPublicationsAsync(List<Publication> publications, string filePath, FileFormatType formatType, CancellationToken cancellationToken = default)
        {
            switch (formatType)
            {
                case FileFormatType.BibTeX:
                    await _bibTeXProcessor.ExportAsync(publications, filePath, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    await _csvProcessor.ExportAsync(publications, filePath, cancellationToken);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Plugins

        public IEnumerable<PluginInfo> GetAllPlugins()
        {
            return _pluginManager
                .GetAllPluginContainers()
                .Select(p => p.Info);
        }

        public IEnumerable<PluginInfo> GetPublicationsCollectorPlugins()
        {
            return _pluginManager
                .GetPluginContainers<IPublicationsCollector>()
                .Select(p => p.Info);
        }

        public async Task<IEnumerable<Publication>> GetPublicationsFromPluginAsync(int pluginId, SearchParametersFromModel searchParameters, CancellationToken cancellationToken = default) 
        {
            var plugin = _pluginManager.GetPlugin<IPublicationsCollector>(pluginId);
            
            if (plugin == null) throw new Exception($"Plugin Id={pluginId} does not exist in this context!");

            var pluginPublications = await plugin.GetPublicationsAsync(_mapper.Map<SearchParametersFromPlugin>(searchParameters), cancellationToken);
            return _mapper.Map<IEnumerable<Publication>>(pluginPublications);
        }

        #endregion

        #region Configuration

        public IEnumerable<EnumerationTable> GetEnumerationTables()
        {
            return _configurationManager.GetEnumerationTables();
        }

        #endregion

        #region Dummy

        /// <summary>
        /// Aplica la regla de filtros Rf1 a una lista.
        /// </summary>
        /// <param name="publications"></param>
        /// <returns></returns>
        public List<Publication> PerformRf1(List<Publication> publications)
        {
#warning usar queries dinámicas con expression trees (https://code-maze.com/dynamic-queries-expression-trees-csharp/) o Dynamic LinQ (https://code-maze.com/using-dynamic-linq/ / https://github.com/zzzprojects/System.Linq.Dynamic.Core)

            var filtered = new List<Publication>();
            var filtered2 = new List<Publication>();

            // Aplicamos los filtros definidos para Rf1...
            filtered = publications.Where(x =>
                x.Abstract is not null && x.Title is not null
                &&
                (
                    // Articulos que tienen en el título "software, development framework*" acompañado opcionalmente por "ontolog* y domain*"...

                    x.Title.Contains("software", StringComparison.InvariantCultureIgnoreCase) &&
                    x.Title.Contains("development", StringComparison.InvariantCultureIgnoreCase) &&
                    (x.Title.Contains("framework", StringComparison.InvariantCultureIgnoreCase) || x.Title.Contains("frameworks", StringComparison.InvariantCultureIgnoreCase))
                    ||
                    x.Title.Contains("software", StringComparison.InvariantCultureIgnoreCase) &&
                    x.Title.Contains("development", StringComparison.InvariantCultureIgnoreCase) &&
                    (x.Title.Contains("framework", StringComparison.InvariantCultureIgnoreCase) || x.Title.Contains("frameworks", StringComparison.InvariantCultureIgnoreCase)) &&
                    (x.Title.Contains("ontology", StringComparison.InvariantCultureIgnoreCase) || x.Title.Contains("ontologies", StringComparison.InvariantCultureIgnoreCase)) &&
                    (x.Title.Contains("domain", StringComparison.InvariantCultureIgnoreCase) || x.Title.Contains("domains", StringComparison.InvariantCultureIgnoreCase))

                    ||
                    // Articulos que tienen en el abstract "software, development framework*" acompañado opcionalmente por "ontolog* y domain*"...

                    x.Abstract.Contains("software", StringComparison.InvariantCultureIgnoreCase) &&
                    x.Abstract.Contains("development", StringComparison.InvariantCultureIgnoreCase) &&
                    x.Abstract.Contains("framework", StringComparison.InvariantCultureIgnoreCase) || x.Abstract.Contains("frameworks", StringComparison.InvariantCultureIgnoreCase)
                    ||
                    x.Abstract.Contains("software", StringComparison.InvariantCultureIgnoreCase) &&
                    x.Abstract.Contains("development", StringComparison.InvariantCultureIgnoreCase) &&
                    (x.Abstract.Contains("framework", StringComparison.InvariantCultureIgnoreCase) || x.Abstract.Contains("frameworks", StringComparison.InvariantCultureIgnoreCase)) &&
                    (x.Abstract.Contains("ontology", StringComparison.InvariantCultureIgnoreCase) || x.Abstract.Contains("ontologies", StringComparison.InvariantCultureIgnoreCase)) &&
                    (x.Abstract.Contains("domain", StringComparison.InvariantCultureIgnoreCase) || x.Abstract.Contains("domains", StringComparison.InvariantCultureIgnoreCase))

                )
            ).ToList();

            // Aplicamos filtro NEAR/ONEAR en los abstract con una distancia máxima de 5 palabras...
            filtered2 = filtered.Where(x =>

                x.Abstract.MinDistanceBetweenWords("software", "development").IsInRange(0, 5) &&
                (x.Abstract.MinDistanceBetweenWords("development", "framework").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("development", "frameworks").IsInRange(0, 5)) &&
                (x.Abstract.MinDistanceBetweenWords("software", "framework").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("software", "frameworks").IsInRange(0, 5))

                ||

                x.Abstract.MinDistanceBetweenWords("ontology", "domain").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("ontologies", "domain").IsInRange(0, 5) ||
                x.Abstract.MinDistanceBetweenWords("ontology", "domains").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("ontologies", "domains").IsInRange(0, 5)

                ).ToList();

            return filtered2;
        }

        #endregion
    }
}