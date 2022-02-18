using System;
using System.Linq;
using System.Reactive.Linq;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.CommunicationChannels.DTOs;
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
        private readonly ICommunicationChannelMessageSubscriptionProvider _communicationChannelMessageSubscriptionProvider;
        private readonly ICommunicationChannelSubscriptionProvider _communicationChannelSubscriptionProvider;
        
        public GraphQLSubscription(ICommunicationChannelMessageSubscriptionProvider communicationChannelMessageSubscriptionProvider,
            ICommunicationChannelSubscriptionProvider communicationChannelSubscriptionProvider)
        {
            _communicationChannelMessageSubscriptionProvider = communicationChannelMessageSubscriptionProvider;
            _communicationChannelSubscriptionProvider = communicationChannelSubscriptionProvider;
            
            Name = "Subscription";

            AddField(new EventStreamFieldType
            {
                Name = "communicationChannelMessageAddedToChannel",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                Type = typeof(CommunicationChannelMessageGraphType),
                Resolver = new FuncFieldResolver<CommunicationChannelMessageDto>(ResolveCommunicationChannelMessage),
                Subscriber = new EventStreamResolver<CommunicationChannelMessageDto>(SubscribeByCommunicationChannelId)
            });
            
            AddField(new EventStreamFieldType
            {
                Name = "communicationChannelAddedForUser",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
                ),
                Type = typeof(CommunicationChannelGraphType),
                Resolver = new FuncFieldResolver<CommunicationChannelDto>(ResolveCommunicationChannel),
                Subscriber = new EventStreamResolver<CommunicationChannelDto>(SubscribeToCommunicationChannelByUserId)
            });
        }

        #region CommunicationChannelMessage 
        
        private CommunicationChannelMessageDto ResolveCommunicationChannelMessage(IResolveFieldContext context)
        {
            var communicationChannelMessage = context.Source as CommunicationChannelMessageDto;

            return communicationChannelMessage;
        }

        private IObservable<CommunicationChannelMessageDto> SubscribeByCommunicationChannelId(
            IResolveEventStreamContext context)
        {
            var channelId = context.GetString("channelId");
            var communicationChannelMessages = _communicationChannelMessageSubscriptionProvider.CommunicationChannelMessages();
            return communicationChannelMessages.Where(m => m.ChannelId == channelId);
        }
        #endregion
        
        private CommunicationChannelDto ResolveCommunicationChannel(IResolveFieldContext context)
        {
            var communicationChannel = context.Source as CommunicationChannelDto;

            return communicationChannel;
        }

        private IObservable<CommunicationChannelDto> SubscribeToCommunicationChannelByUserId(
            IResolveEventStreamContext context)
        {
            var userId = context.GetString("userId");
            var communicationChannels = _communicationChannelSubscriptionProvider.CommunicationChannels();
            return communicationChannels.Where(m => m.ParticipantIds.Contains(userId));
        }
    }
}