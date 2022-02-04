using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Application.CommunicationChannels.Queries.GetById;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CommunicationChannelQuery : ObjectGraphType
    {
        public CommunicationChannelQuery()
        {
            Name = "CommunicationChannelQuery";
            this.FieldAsyncWithScope<CommunicationChannelGraphType, CommunicationChannelDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CommunicationChannelGraphType, CommunicationChannelDto>, PaginatedList<CommunicationChannelDto>>(
                "getForUser",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelsForUserQuery()
                    {
                        UserId = ctx.GetString("userId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}