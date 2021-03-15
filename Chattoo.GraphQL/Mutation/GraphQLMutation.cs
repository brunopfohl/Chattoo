using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class GraphQLMutation : ObjectGraphType
    {
        public GraphQLMutation()
        {
            Name = "Mutation";

            //Field<TodoItemMutation>("todoItems", resolve: context => new { });
            //Field<TodoListMutation>("todoLists", resolve: context => new { });
        }
    }
}
