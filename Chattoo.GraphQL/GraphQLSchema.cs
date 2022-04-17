using Chattoo.GraphQL.Mutation;
using Chattoo.GraphQL.Query;
using GraphQL.Types;
using GraphQL.Utilities;
using System;
using Chattoo.Domain.Interfaces;
using Chattoo.GraphQL.Subscription;
using Chattoo.GraphQL.Subscription.CalendarEvent;
using Chattoo.GraphQL.Subscription.CommunicationChannel;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;

namespace Chattoo.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider,
            ICommunicationChannelMessageSubscriptionProvider communicationChannelMessageSubscriptionProvider,
            ICommunicationChannelSubscriptionProvider communicationChannelSubscriptionProvider,
            ICalendarEventSubscriptionProvider calendarEventSubscriptionProvider,
            ICurrentUserIdService currentUserIdService)
            : base(provider)
        {
            Query = provider.GetRequiredService<GraphQLQuery>();
            Mutation = provider.GetRequiredService<GraphQLMutation>();
            Subscription = new GraphQLSubscription
            (
                communicationChannelMessageSubscriptionProvider,
                communicationChannelSubscriptionProvider,
                calendarEventSubscriptionProvider,
                currentUserIdService
            );
        }
    }
}
