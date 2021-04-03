using Chattoo.Application.GroupRoles.Queries.GetForUser;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Application.Groups.Queries.GetById;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class GroupQuery : ObjectGraphType
    {
        public GroupQuery()
        {
            Name = "GroupQuery";
            
            this.FieldAsyncWithScope<GroupType, object, object>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetGroupByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<GroupType, GroupDto>, object, object>(
                "getForUser",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetGroupsForUserQuery()
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