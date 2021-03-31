using GraphQL.Types;

namespace Chattoo.GraphQL.Arguments
{
    public class QueryArgumentsWithPagination : QueryArguments
    {
        public QueryArgumentsWithPagination(params QueryArgument[] args) : this(10, args)
        {
        }
        
        public QueryArgumentsWithPagination(int defaultPageSize, params QueryArgument[] args)
        {
            Add(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageNumber", DefaultValue = 1});
            Add(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageSize", DefaultValue = defaultPageSize});
        }
    }
}