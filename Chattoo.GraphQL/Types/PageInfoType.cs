using Chattoo.Application.Common.Models;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class PageInfoType<TType, TDto> : ObjectGraphType<PaginatedList<TType>> where TType : ObjectGraphType<TDto>
    {
        public PageInfoType(string name)
        {
            Field<ListGraphType<TType>>(
                name,
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