using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannelMessages.Queries.GetById;
using Chattoo.Application.CommunicationChannelRoles.DTOs;
using Chattoo.Application.CommunicationChannels.Queries.GetRole;
using Chattoo.Application.Users.Queries.GetChannelRoles;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CommunicationChannelRoleQuery : ObjectGraphType
    {
        public CommunicationChannelRoleQuery()
        {
            Name = "CommunicationChannelRoleQuery";
            
            this.FieldAsyncWithScope<CommunicationChannelRoleGraphType, CommunicationChannelRoleDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelRoleByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CommunicationChannelRoleGraphType, CommunicationChannelRoleDto>, PaginatedList<CommunicationChannelRoleDto>>(
                "getForUserInChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelRolesForUserInChannelQuery()
                    {
                        UserId = ctx.GetString("userId"),
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