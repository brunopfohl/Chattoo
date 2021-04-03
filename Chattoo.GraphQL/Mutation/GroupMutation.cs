using Chattoo.Application.Groups.Commands.Create;
using Chattoo.Application.Groups.Commands.Delete;
using Chattoo.Application.Groups.Commands.Update;
using Chattoo.GraphQL.Extensions;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class GroupMutation : ObjectGraphType
    {
        public GroupMutation()
        {
            Name = "GroupMutation";
            
            this.FieldAsyncWithScope<StringGraphType, string>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new CreateGroupCommand()
                    {
                        Name = ctx.GetString("name")
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
                    var command = new DeleteGroupCommand()
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateGroupCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}