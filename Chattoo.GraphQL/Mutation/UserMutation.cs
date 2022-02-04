using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class UserMutation : ObjectGraphType
    {
        public UserMutation()
        {
            Name = "UserMutation";
        }
    }
}