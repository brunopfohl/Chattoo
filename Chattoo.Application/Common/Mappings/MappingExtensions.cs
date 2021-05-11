using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chattoo.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        public static Task<PaginatedList<TDestination>> PaginatedListOrderedAsync<TDestination, TKey>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, Func<TDestination, TKey> keySelector)
            where  TKey : IComparable
            => PaginatedList<TDestination>.CreateAsyncOrdered(queryable, pageNumber, pageSize, keySelector);
        
        public static Task<PaginatedList<TDestination>> PaginatedListOrderedDescendingAsync<TDestination, TKey>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, Func<TDestination, TKey> keySelector)
            where TKey : IComparable
            => PaginatedList<TDestination>.CreateAsyncOrderedDescending(queryable, pageNumber, pageSize, keySelector);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
