using Chattoo.Application.Groups.Commands;
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
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "addUser",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new AddUserToGroupCommand()
                    {
                        UserId = ctx.GetString("userId"),
                        GroupId = ctx.GetString("groupId")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "removeUser",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new RemoveUserFromGroupCommand()
                    {
                        UserId = ctx.GetString("userId"),
                        GroupId = ctx.GetString("groupId")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}