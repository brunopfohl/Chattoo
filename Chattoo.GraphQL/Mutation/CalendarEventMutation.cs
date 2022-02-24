using System;
using Chattoo.Application.CalendarEvents.Commands;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Domain.Enums;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using Chattoo.GraphQL.Types.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CalendarEventMutation : ObjectGraphType
    {
        public CalendarEventMutation()
        {
            Name = "CalendarEventMutation";
            
            this.FieldAsyncWithScope<CalendarEventGraphType, CalendarEventDto>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "desc" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "startsAt" },
                    new QueryArgument<DateTimeGraphType> { Name = "endsAt" },
                    new QueryArgument<StringGraphType> { Name = "channelId" },
                    new QueryArgument<StringGraphType> { Name = "groupId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "type" },
                    new QueryArgument<IntGraphType> { Name = "maximalParticipantsCount" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    // TODO: Tady to asi chce, mít přímo nějaký vlastní input type.
                    if (!Enum.TryParse(ctx.GetString("type"), out CalendarEventType type))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    
                    var command = new CreateCalendarEventCommand()
                    {
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc"),
                        StartsAt = ctx.GetDateTime("startsAt"),
                        EndsAt = ctx.GetNullableDateTime("endsAt"),
                        CommunicationChannelId = ctx.GetString("channelId"),
                        GroupId = ctx.GetString("groupId"),
                        Type = type,
                        MaximalParticipantsCount = ctx.GetNullableInt("maximalParticipantsCount")
                    };

                    var calendarEvent = await mediator.Send(command);

                    return calendarEvent;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "delete",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new DeleteCalendarEventCommand()
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "desc" },
                    new QueryArgument<DateTimeGraphType> { Name = "startsAt" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "endsAt" },
                    new QueryArgument<IntGraphType> { Name = "maximalParticipantsCount" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateCalendarEventCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc"),
                        StartsAt =  ctx.GetDateTime("startsAt"),
                        EndsAt =  ctx.GetNullableDateTime("endsAt"),
                        MaximalParticipantsCount = ctx.GetNullableInt("maximalParticipantsCount")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}