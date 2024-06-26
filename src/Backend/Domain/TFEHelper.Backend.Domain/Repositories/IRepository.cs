﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.Domain.Repositories
{
    public interface IRepository
    {
        Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null, bool tracked = true, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        PaginatedList<T> GetListPaginated<T>(PaginationParameters parameters, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;

        Task<T> UpdateAsync<T>(T publication, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task RemoveAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ITFEHelperModel;

        Task<List<T>> RunDatabaseQueryAsync<T>(string query, List<IDatabaseParameter>? parameters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] navigationProperties) where T : class, ITFEHelperModel;
    }
}
