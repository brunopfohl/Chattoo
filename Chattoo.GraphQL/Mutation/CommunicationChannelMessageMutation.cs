using Chattoo.Application.CommunicationChannelMessages.Commands.Create;
using Chattoo.Application.CommunicationChannelMessages.Commands.Delete;
using Chattoo.Application.CommunicationChannelMessages.Commands.Update;
using Chattoo.Domain.Enums;
using Chattoo.GraphQL.Extensions;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CommunicationChannelMessageMutation : ObjectGraphType
    {
        public CommunicationChannelMessageMutation()
        {
            Name = "CommunicationChannelMessageMutation";
            
            this.FieldAsyncWithScope<StringGraphType, string>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "content" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "type" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new CreateCommunicationChannelMessageCommand()
                    {
                        UserId = ctx.GetString("userId"),
                        ChannelId = ctx.GetString("channelId"),
                        Content = ctx.GetString("content"),
                        Type = (CommunicationChannelMessageType)ctx.GetInt("type")
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
                    var command = new DeleteCommunicationChannelMessageCommand()
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
                    var command = new UpdateCommunicationChannelMessageCommand()
                    {
                        Id = ctx.GetString("id"),
                        Content = ctx.GetString("content")
                    };

                    await mediator.Send(command);

                    return true;
                }
            );
        }
    }
}