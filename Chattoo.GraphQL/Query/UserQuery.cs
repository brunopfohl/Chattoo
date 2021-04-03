using Chattoo.Application.Common.Models;
using Chattoo.Application.Users.DTOs;
using Chattoo.Application.Users.Queries.GetForCommunicationChannel;
using Chattoo.Application.Users.Queries.GetForGroup;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery()
        {
            Name = "UserQuery";
            
            this.FieldAsyncWithScope<PageInfoType<UserType, UserDto>, PaginatedList<UserDto>>(
                "getForCommunicationChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersForCommunicationChannelQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<UserType, UserDto>, PaginatedList<UserDto>>(
                "getForGroup",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersForGroupQuery()
                    {
                        GroupId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}