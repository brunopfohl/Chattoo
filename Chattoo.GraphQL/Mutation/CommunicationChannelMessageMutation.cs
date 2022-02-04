using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.Application.CommunicationChannels.Commands.UpdateMessage;
using Chattoo.Application.Users.Commands.AddMessage;
using Chattoo.Domain.Enums;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CommunicationChannelMessageMutation : ObjectGraphType
    {
        public CommunicationChannelMessageMutation(ICommunicationChannelMessageSubscriptionProvider communicationChannelMessageSubscriptionProvider)
        {
            Name = "CommunicationChannelMessageMutation";
            
            this.FieldAsyncWithScope<CommunicationChannelMessageGraphType, CommunicationChannelMessageDto>(
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

                    var communicationChannelMessageDto = await mediator.Send(command);

                    // Přidám zprávu do kolekce přidaných zpráv (v GraphQL kontextu), aby se mohla zaslat "notifikace" subscriberům.
                    communicationChannelMessageSubscriptionProvider.AddCommunicationChannelMessage(
                        communicationChannelMessageDto);

                    return communicationChannelMessageDto;
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
                    var command = new UpdateChannelMessageCommand()
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