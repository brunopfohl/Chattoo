using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannels.Queries;
using Chattoo.Application.CommunicationChannels.Queries.GetRoles;
using Chattoo.Application.Users.Queries;
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelRoleByIdQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        RoleId = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<CommunicationChannelRoleGraphType, CommunicationChannelRoleDto>, PaginatedList<CommunicationChannelRoleDto>>(
                "getForChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetCommunicationChannelRolesQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
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