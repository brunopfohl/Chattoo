using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannelCalendarEvents.DTOs;
using Chattoo.Application.CommunicationChannelCalendarEvents.Queries.GetById;
using Chattoo.Application.CommunicationChannelCalendarEvents.Queries.GetForCommunicationChannel;
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
            this.FieldAsyncWithScope<CommunicationChannelCalendarEventType, CommunicationChannelCalendarEventDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelCalendarEventByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<CommunicationChannelCalendarEventType, CommunicationChannelCalendarEventDto>, PaginatedList<CommunicationChannelCalendarEventDto>>(
                "getForCommunicationChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelCalendarEventsForCommunicationChannelQuery()
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