using System;
using Chattoo.Application.Common.Models;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class PageInfoType<TType, TDto> : ObjectGraphType<PaginatedList<TType>> where TType : ObjectGraphType<TDto>
    {
        public PageInfoType()
        {
            Name = $"PaginationList{typeof(TType).Name}";

            Field<ListGraphType<TType>>(
                "data",
                resolve: context => context.Source.Items
            );

            Field(x => x.PageIndex);
            Field(x => x.HasPreviousPage);
            Field(x => x.HasNextPage);
            Field(x => x.TotalCount);
            Field(x => x.TotalPages);
        }
    }
}