using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using GraphQL.Types;

namespace Chattoo.GraphQL.Subscription
{
    public class GraphQLSubscription : ObjectGraphType
    {
        public GraphQLSubscription()
        {
            Name = "Subscription";

            Field<CommunicationChannelMessageSubscriptions>("communicationChannelMessages",
                resolve: context => new { });
        }
    }
}