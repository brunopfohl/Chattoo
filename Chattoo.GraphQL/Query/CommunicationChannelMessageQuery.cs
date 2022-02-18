using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannels.Queries;
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "messageId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetMessageFromChannelQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        MessageId = ctx.GetString("id")
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

                    var result = await mediator.Send(query);
                    return result;
                }
            );
        }
    }
}