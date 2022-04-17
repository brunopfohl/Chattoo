using System;
using System.Reactive.Linq;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Interfaces;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Subscription.CalendarEvent;
using Chattoo.GraphQL.Subscription.CommunicationChannel;
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
        private readonly ICalendarEventSubscriptionProvider _calendarEventSubscriptionProvider;
        private readonly ICurrentUserIdService _currentUserId;
        
        public GraphQLSubscription(ICommunicationChannelMessageSubscriptionProvider communicationChannelMessageSubscriptionProvider,
            ICommunicationChannelSubscriptionProvider communicationChannelSubscriptionProvider, ICalendarEventSubscriptionProvider calendarEventSubscriptionProvider, ICurrentUserIdService currentUserId)
        {
            _communicationChannelMessageSubscriptionProvider = communicationChannelMessageSubscriptionProvider;
            _communicationChannelSubscriptionProvider = communicationChannelSubscriptionProvider;
            _calendarEventSubscriptionProvider = calendarEventSubscriptionProvider;
            _currentUserId = currentUserId;

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
            
            AddField(new EventStreamFieldType
            {
                Name = "userAddedToEvent",
                Arguments = new QueryArguments(),
                Type = typeof(CalendarEventGraphType),
                Resolver = new FuncFieldResolver<CalendarEventDto>(ResolveCalendarEvent),
                Subscriber = new EventStreamResolver<CalendarEventDto>(SubscribeToCalendarEvents)
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

        #region CommunicationChanel
        
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

        #endregion

        #region CalendarEvent

        private CalendarEventDto ResolveCalendarEvent(IResolveFieldContext context)
        {
            var calendarEvent = context.Source as CalendarEventDto;

            return calendarEvent;
        }

        private IObservable<CalendarEventDto> SubscribeToCalendarEvents(
            IResolveEventStreamContext context)
        {
            var result = _calendarEventSubscriptionProvider
                .CalendarEvents()
                .Where(e => e.UserId == _currentUserId.UserId)
                .Select(e => e.CalendarEvent);

            return result;
        }

        #endregion
    }
}