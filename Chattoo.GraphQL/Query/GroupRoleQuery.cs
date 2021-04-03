using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Application.GroupRoles.Queries.GetById;
using Chattoo.Application.GroupRoles.Queries.GetForUserInGroup;
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
            
            this.FieldAsyncWithScope<GroupRoleType, object, object>(
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

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<GroupRoleType, GroupRoleDto>, object, object>(
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

                    return await mediator.Send(query);
                }
            );
        }
    }
}