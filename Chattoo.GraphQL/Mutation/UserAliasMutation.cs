using Chattoo.Application.Users.Commands.AddAlias;
using Chattoo.Application.Users.Commands.DeleteAlias;
using Chattoo.Application.Users.Commands.UpdateAlias;
using Chattoo.GraphQL.Extensions;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class UserAliasMutation : ObjectGraphType
    {
        public UserAliasMutation()
        {
            Name = "UserAliasMutation";
            
            this.FieldAsyncWithScope<StringGraphType, string>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "alias" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new AddUserAliasCommand()
                    {
                         Alias = ctx.GetString("alias")
                    };

                    var id = await mediator.Send(command);

                    return id;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "delete",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new DeleteUserAliasCommand()
                    {
                        Id = ctx.GetString("id")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "update",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "alias" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateUserAliasCommand()
                    {
                        Id = ctx.GetString("id"),
                        Alias = ctx.GetString("name")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}