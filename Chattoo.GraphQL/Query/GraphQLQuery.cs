using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        public GraphQLQuery()
        {
            Name = "Query";
            
        }
    }
}
