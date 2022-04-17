using System;
using System.Collections.Generic;
using Chattoo.Application.CalendarEventWishes.Commands;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Subscription.CalendarEvent;
using Chattoo.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CalendarEventWishMutation : ObjectGraphType
    {
        private readonly ICalendarEventSubscriptionProvider _calendarEventSubscriptionProvider;
        
        // TODO: Dodělat. Zatím mě drží zpátky, že potřebuji mít možnost zaslat kolekci časových intervalů.
        public CalendarEventWishMutation(ICalendarEventSubscriptionProvider calendarEventSubscriptionProvider)
        {
            _calendarEventSubscriptionProvider = calendarEventSubscriptionProvider;
            Name = "CalendarEventWishMutation";
            
            this.FieldAsyncWithScope<CalendarEventWishGraphType, CalendarEventWishDto>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "type" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "minimalParticipantsCount" },
                    new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "minimalLengthInMinutes" },
                    new QueryArgument<NonNullGraphType<ListGraphType<DateIntervalInputGraphType>>> { Name = "dateIntervals" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    // TODO: Tady to asi chce, mít přímo nějaký vlastní input type.
                    if (!Enum.TryParse(ctx.GetString("type"), out CalendarEventType type))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    
                    var minimalLengthInMinutes = ctx.GetLong("minimalLengthInMinutes");
                    var minimalLength = TimeSpan.FromMinutes(minimalLengthInMinutes);
                    
                    var command = new CreateCalendarEventWishCommand()
                    {
                        CommunicationChannelId = ctx.GetString("channelId"),
                        Name = ctx.GetString("name"),
                        Type = type,
                        MinimalLength = minimalLength,
                        MinimalParticipantsCount = ctx.GetInt("minimalParticipantsCount"),
                        DateIntervals = ctx.GetArgument<List<DateIntervalDto>>("dateIntervals")
                    };

                    var response = await mediator.Send(command);

                    if (response.JoinedEvent is not null)
                    {
                        
                    }

                    return response.CreatedWish;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "delete",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new DeleteCalendarEventWishCommand()
                    {
                        Id = ctx.GetString("id")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );

            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "update",
                arguments:
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "type" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "minimalParticipantsCount" },
                    new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "minimalLengthInMinutes" },
                    new QueryArgument<ListGraphType<DateIntervalInputGraphType>> { Name = "dateIntervals" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    // TODO: Tady to asi chce, mít přímo nějaký vlastní input type.
                    if (!Enum.TryParse(ctx.GetString("type"), out CalendarEventType type))
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    var minimalLengthInMinutes = ctx.GetLong("minimalLengthInMinutes");
                    var minimalLength = TimeSpan.FromMinutes(minimalLengthInMinutes);
                    
                    var command = new UpdateCalendarEventWishCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name"),
                        Type = type,
                        MinimalLength = minimalLength,
                        MinimalParticipantsCount = ctx.GetInt("minimalParticipantsCount"),
                        DateIntervals = ctx.GetArgument<List<DateIntervalDto>>("dateIntervals")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}