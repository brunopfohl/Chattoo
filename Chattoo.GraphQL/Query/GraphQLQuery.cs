using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        public GraphQLQuery()
        {
            Name = "Query";

            //Field<TodoItemQuery>("todoItems", resolve: context => new { });
            //Field<TodoListQuery>("todoLists", resolve: context => new { });
        }
    }
}
