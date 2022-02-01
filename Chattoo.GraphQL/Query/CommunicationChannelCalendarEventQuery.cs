using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEvents.Queries;
using Chattoo.Application.Common.Models;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CommunicationChannelCalendarEventQuery : ObjectGraphType
    {
        public CommunicationChannelCalendarEventQuery()
        {
            
            Name = "CommunicationChannelCalendarEventQuery";
            this.FieldAsyncWithScope<CommunicationChannelCalendarEventGraphType, CalendarEventDto>(
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
            
            this.FieldAsyncWithScope<PageInfoGraphType<CommunicationChannelCalendarEventGraphType, CalendarEventDto>, PaginatedList<CalendarEventDto>>(
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
                        CommunicationChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}