using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Groups.Queries;
using Chattoo.Application.Users.Queries;
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "roleId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetGroupRoleByIdQuery()
                    {
                        GroupId = ctx.GetString("groupId"),
                        RoleId = ctx.GetString("roleId")
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