using Chattoo.Application.CommunicationChannelRoles.Commands.Create;
using Chattoo.Application.CommunicationChannelRoles.Commands.Delete;
using Chattoo.Application.CommunicationChannelRoles.Commands.Update;
using Chattoo.Domain.Enums;
using Chattoo.GraphQL.Extensions;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CommunicationChannelRoleMutation : ObjectGraphType
    {
        public CommunicationChannelRoleMutation()
        {
            Name = "CommunicationChannelRoleMutation";
            
            this.FieldAsyncWithScope<StringGraphType, string>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "permission" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new CreateCommunicationChannelRoleCommand()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        Name = ctx.GetString("name"),
                        Permission = (CommunicationChannelPermission)ctx.GetInt("permission"),
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
                    var command = new DeleteCommunicationChannelRoleCommand()
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "content" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateCommunicationChannelRoleCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name"),
                        Permission = (CommunicationChannelPermission)ctx.GetInt("permission")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}