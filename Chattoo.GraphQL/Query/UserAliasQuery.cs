using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Users.Queries;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class UserAliasQuery : ObjectGraphType
    {
        public UserAliasQuery()
        {
            Name = "UserAliasQuery";
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserAliasGraphType, UserAliasDto>, PaginatedList<UserAliasDto>>(
                "getForUser",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetAliasesForUserQuery()
                    {
                        UserId = ctx.GetString("userId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    var userAliases = await mediator.Send(query);
                    return userAliases;
                }
            );
        }
    }
}