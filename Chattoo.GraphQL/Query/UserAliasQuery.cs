using Chattoo.Application.Common.Models;
using Chattoo.Application.UserAliases.DTOs;
using Chattoo.Application.UserAliases.Queries.GetById;
using Chattoo.Application.UserAliases.Queries.GetForUser;
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
            
            this.FieldAsyncWithScope<UserAliasGraphType, UserAliasDto>(
                "get",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUserAliasByIdQuery()
                    {
                        Id = ctx.GetString("id")
                    };

                    var userAlias = await mediator.Send(query);
                    return userAlias;
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserAliasGraphType, UserAliasDto>, PaginatedList<UserAliasDto>>(
                "getForUser",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUserAliasesForUserQuery()
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