using System;
using System.Reactive.Linq;
using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Subscription;
using GraphQL.Types;

namespace Chattoo.GraphQL.Subscription.CommunicationChannelMessage
{
    public class CommunicationChannelMessageSubscriptions : ObjectGraphType
    {
        private readonly ICommunicationChannelMessageSubscriptionProvider _provider;

        public CommunicationChannelMessageSubscriptions(ICommunicationChannelMessageSubscriptionProvider provider)
        {
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