using Chattoo.Application.Common.Models;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Application.Groups.Queries.GetRole;
using Chattoo.Application.Users.Queries.GetGroupRoles;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class GroupRoleQuery : ObjectGraphType
    {
        public GroupRoleQuery()
        {
            Name = "GroupRoleQuery";
            
            this.FieldAsyncWithScope<GroupRoleGraphType, GroupRoleDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetGroupRoleByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    var groupRole = await mediator.Send(query);
                    return groupRole;
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<GroupRoleGraphType, GroupRoleDto>, PaginatedList<GroupRoleDto>>(
                "getForUserInGroup",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetGroupRolesForUserInGroupQuery()
                    {
                        UserId = ctx.GetString("userId"),
                        GroupId = ctx.GetString("groupId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    var groupRoles = await mediator.Send(query);
                    return groupRoles;
                }
            );
        }
    }
}