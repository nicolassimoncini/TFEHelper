﻿using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Core.Processors.BibTeX;
using TFEHelper.Backend.Core.Processors.CSV;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Extensions;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;
using ModelValidator = TFEHelper.Backend.Tools.ComponentModel.ModelValidator;

namespace TFEHelper.Backend.Core.Engine.Implementations
{
    public sealed class TFEHelperEngine : ITFEHelperEngine
    {
        private List<Publication> _publications;
        private readonly ILogger<TFEHelperEngine> _logger;
        private readonly IEntitiesManager _entitiesManager;
        private readonly IMapper _mapper;
        private readonly BibTeXProcessor _bibTeXProcessor;
        private readonly CSVProcessor _csvProcessor;

        public TFEHelperEngine(ILogger<TFEHelperEngine> logger, IEntitiesManager entitiesManager, IMapper mapper)
        {
            _logger = logger;
            _entitiesManager = entitiesManager;
            _mapper = mapper;
            _bibTeXProcessor = new BibTeXProcessor();
            _csvProcessor = new CSVProcessor();

            _publications = new List<Publication>();
        }
        
        /*
        // Ojo, esta capa no tiene que saber cómo dialogar con quien la invocó.  Por eso, no debería mapear hacia DTO.
        // La capa exterior es la responsable de mapear entidades hacia adentro y no al revés...
        public async Task<D> UpdateAsync<D, M>(M entity, CancellationToken cancellationToken = default)
            where D : class, ITFEHelperDTO, new()
            where M : class, ITFEHelperModel            
        {
            await _repository.UpdateAsync<M>(entity, cancellationToken);
            await _repository.SaveAsync<M>(cancellationToken);
            return _mapper.Map<D>(entity);
        }
        */

        public async Task ImportPublicationsAsync(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default)
        {
            switch (formatType)
            {
                case FileFormatType.BibTeX:
                    _publications = await _bibTeXProcessor.ImportAsync(filePath, source, cancellationToken);
                    break;
                case FileFormatType.CSV:
                    _publications = await _csvProcessor.ImportAsync(filePath, source, cancellationToken);
                    break;
                default:
                    break;
            }

            if (discardInvalidRecords)
            {
                _publications.RemoveAll(p => !ModelValidator.IsModelValid(p));
            }

            await _entitiesManager.CreateRangeAsync(_publications, cancellationToken);
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

        /// <summary>
        /// Aplica la regla de filtros Rf1 a una lista.
        /// </summary>
        /// <param name="publications"></param>
        /// <returns></returns>
        public List<Publication> PerformRf1(List<Publication> publications)
        {
#warning usar "contains" de Dynamic Linq Library (buscar en nuget / https://dynamic-linq.net/)
#warning usar queries dinámicas con expression trees (https://code-maze.com/dynamic-queries-expression-trees-csharp/)

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

        public void Dummy()
        {
            // Vamos a jugar un poco con los filtros...
            var filtered = _publications.Where(x =>
                x.Abstract is not null && x.Title is not null && x.Source == SearchSourceType.SEDICI
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
            var filtered2 = filtered.Where(x =>

                x.Abstract.MinDistanceBetweenWords("software", "development").IsInRange(0, 5) &&
                (x.Abstract.MinDistanceBetweenWords("development", "framework").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("development", "frameworks").IsInRange(0, 5)) &&
                (x.Abstract.MinDistanceBetweenWords("software", "framework").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("software", "frameworks").IsInRange(0, 5))

                ||

                x.Abstract.MinDistanceBetweenWords("ontology", "domain").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("ontologies", "domain").IsInRange(0, 5) ||
                x.Abstract.MinDistanceBetweenWords("ontology", "domains").IsInRange(0, 5) || x.Abstract.MinDistanceBetweenWords("ontologies", "domains").IsInRange(0, 5)

                ).ToList();
        }
    }
}