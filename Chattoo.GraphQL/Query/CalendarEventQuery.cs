using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEvents.Queries;
using Chattoo.Application.CalendarEvents.Queries.Get;
using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannels.Queries;
using Chattoo.Application.Groups.Queries;
using Chattoo.Application.Users.Queries.GetJoinedEventsQuery.cs;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CalendarEventQuery : ObjectGraphType
    {
        public CalendarEventQuery()
        {
            
            Name = "CalendarEventQuery";
            
            this.FieldAsyncWithScope<PageInfoGraphType<CalendarEventGraphType, CalendarEventDto>, PaginatedList<CalendarEventDto>>(
                "getVisible",
                arguments: 
                new QueryArgumentsWithPagination(),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCalendarEventsQuery()
                    {
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CalendarEventGraphType, CalendarEventDto>, PaginatedList<CalendarEventDto>>(
                "getJoined",
                arguments: 
                new QueryArgumentsWithPagination(),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetJoinedEventsQuery()
                    {
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<CalendarEventGraphType, CalendarEventDto>(
                "getAvailable",
                arguments: 
                new QueryArguments(),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCalendarEventByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<CalendarEventGraphType, CalendarEventDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCalendarEventByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CalendarEventGraphType, CalendarEventDto>, PaginatedList<CalendarEventDto>>(
                "getForCommunicationChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCalendarEventsForCommunicationChannelQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CalendarEventGraphType, CalendarEventDto>, PaginatedList<CalendarEventDto>>(
                "getForGroup",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCalendarEventsForGroupQuery()
                    {
                        GroupId = ctx.GetString("groupId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}