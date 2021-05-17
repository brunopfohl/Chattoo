using Chattoo.GraphQL.Mutation;
using Chattoo.GraphQL.Query;
using GraphQL.Types;
using GraphQL.Utilities;
using System;
using Chattoo.GraphQL.Subscription;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;

namespace Chattoo.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider, ICommunicationChannelMessageSubscriptionProvider communicationChannelMessageSubscriptionProvider, ICommunicationChannelSubscriptionProvider communicationChannelSubscriptionProvider)
            : base(provider)
        {
            Query = provider.GetRequiredService<GraphQLQuery>();
            Mutation = provider.GetRequiredService<GraphQLMutation>();
            Subscription = new GraphQLSubscription(communicationChannelMessageSubscriptionProvider, communicationChannelSubscriptionProvider);
        }
    }
}
