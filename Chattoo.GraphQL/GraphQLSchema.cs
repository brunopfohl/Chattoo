using Chattoo.GraphQL.Mutation;
using Chattoo.GraphQL.Query;
using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace Chattoo.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<GraphQLQuery>();
            Mutation = provider.GetRequiredService<GraphQLMutation>();
        }
    }
}
