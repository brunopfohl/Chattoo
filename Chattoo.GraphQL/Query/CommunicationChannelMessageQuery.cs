using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.Application.CommunicationChannelMessages.Queries.GetById;
using Chattoo.Application.CommunicationChannelMessages.Queries.GetForChannel;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CommunicationChannelMessageQuery : ObjectGraphType
    {
        public CommunicationChannelMessageQuery()
        {
            Name = "CommunicationChannelMessageQuery";
            
            this.FieldAsyncWithScope<CommunicationChannelMessageGraphType, CommunicationChannelMessageDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelMessageByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CommunicationChannelMessageGraphType, CommunicationChannelMessageDto>, PaginatedList<CommunicationChannelMessageDto>>(
                "getForChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelMessagesForChannelQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}