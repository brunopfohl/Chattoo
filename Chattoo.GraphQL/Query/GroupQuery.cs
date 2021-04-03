using Chattoo.Application.Common.Models;
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
            
            this.FieldAsyncWithScope<GroupType, GroupDto>(
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

                    var group = await mediator.Send(query);
                    return group;
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<GroupType, GroupDto>, PaginatedList<GroupDto>>(
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

                    var groups = await mediator.Send(query);
                    return groups;
                }
            );
        }
    }
}