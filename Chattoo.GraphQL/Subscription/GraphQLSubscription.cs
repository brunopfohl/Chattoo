using System;
using System.Reactive.Linq;
using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using Chattoo.GraphQL.Types;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;

namespace Chattoo.GraphQL.Subscription
{
    public class GraphQLSubscription : ObjectGraphType
    {
        private readonly ICommunicationChannelMessageSubscriptionProvider _provider;
        
        public GraphQLSubscription(ICommunicationChannelMessageSubscriptionProvider provider)
        {
            Name = "Subscription";

            _provider = provider;
            AddField(new EventStreamFieldType
            {
                Name = "communicationChannelMessageAddedToChannel",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                Type = typeof(CommunicationChannelMessageType),
                Resolver = new FuncFieldResolver<CommunicationChannelMessageDto>(ResolveCommunicationChannelMessage),
                Subscriber = new EventStreamResolver<CommunicationChannelMessageDto>(SubscribeByCommunicationChannelId)
            });
        }

        private CommunicationChannelMessageDto ResolveCommunicationChannelMessage(IResolveFieldContext context)
        {
            var communicationChannelMessage = context.Source as CommunicationChannelMessageDto;

            return communicationChannelMessage;
        }

        private IObservable<CommunicationChannelMessageDto> SubscribeByCommunicationChannelId(
            IResolveEventStreamContext context)
        {
            var channelId = context.GetString("channelId");
            var communicationChannelMessages = _provider.CommunicationChannelMessages();
            return communicationChannelMessages.Where(m => m.ChannelId == channelId);
        }
    }
}