using System;
using Chattoo.Application.Common.Models;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class PageInfoGraphType<TType, TDto> : ObjectGraphType<PaginatedList<TDto>> where TType : ObjectGraphType<TDto>
    {
        public PageInfoGraphType()
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