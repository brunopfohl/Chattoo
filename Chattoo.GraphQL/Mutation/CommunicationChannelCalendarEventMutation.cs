using Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Create;
using Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Delete;
using Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Update;
using Chattoo.Application.CommunicationChannelCalendarEvents.DTOs;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CommunicationChannelCalendarEventMutation : ObjectGraphType
    {
        public CommunicationChannelCalendarEventMutation()
        {
            Name = "CommunicationChannelCalendarEventMutation";
            
            this.FieldAsyncWithScope<CommunicationChannelCalendarEventType, CommunicationChannelCalendarEventDto>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "desc" },
                    new QueryArgument<DateGraphType> { Name = "startsAt" },
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name = "endsAt" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new CreateCommunicationChannelCalendarEventCommand()
                    {
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc"),
                        StartsAt = ctx.GetDateTime("startsAt"),
                        EndsAt = ctx.GetNullableDateTime("endsAt"),
                        CommunicationChannelId = ctx.GetString("channelId")
                    };

                    var channel = await mediator.Send(command);

                    return channel;
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
                    var command = new DeleteCommunicationChannelCalendarEventCommand()
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
                    new QueryArgument<DateGraphType> { Name = "startsAt" },
                    new QueryArgument<NonNullGraphType<DateGraphType>> { Name = "endsAt" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateCommunicationChannelCalendarEventCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc"),
                        StartsAt =  ctx.GetDateTime("startsAt"),
                        EndsAt =  ctx.GetNullableDateTime("endsAt")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}