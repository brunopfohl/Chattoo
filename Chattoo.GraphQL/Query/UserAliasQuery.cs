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
            
            this.FieldAsyncWithScope<UserAliasType, object, object>(
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

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoType<UserAliasType, UserAliasDto>, object, object>(
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

                    return await mediator.Send(query);
                }
            );
        }
    }
}