using Chattoo.Application.CommunicationChannels.Commands;
using Chattoo.Application.CommunicationChannels.Commands.AddUser;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Subscription.CommunicationChannel;
using Chattoo.GraphQL.Subscription.CommunicationChannelMessage;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CommunicationChannelMutation : ObjectGraphType
    {
        public CommunicationChannelMutation(ICommunicationChannelSubscriptionProvider communicationChannelSubscriptionProvider)
        {
            var communicationChannelSubscriptionProvider1 = communicationChannelSubscriptionProvider;
            Name = "CommunicationChannelMutation";
            
            this.FieldAsyncWithScope<CommunicationChannelGraphType, CommunicationChannelDto>(
                "create",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "desc" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new CreateCommunicationChannelCommand()
                    {
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc")
                    };

                    var channel = await mediator.Send(command);

                    communicationChannelSubscriptionProvider1.UpdateCommunicationChannel(channel);

                    return channel;
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
                    var command = new DeleteCommunicationChannelCommand()
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "desc" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new UpdateCommunicationChannelCommand()
                    {
                        Id = ctx.GetString("id"),
                        Name = ctx.GetString("name"),
                        Description = ctx.GetString("desc"),
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
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new AddUserToCommunicationChannelCommand()
                    {
                        UserId = ctx.GetString("userId"),
                        ChannelId = ctx.GetString("channelId")
                    };

                    var channel = await mediator.Send(command);
                    
                    communicationChannelSubscriptionProvider1.UpdateCommunicationChannel(channel);

                    return true;
                }
            );
            
            this.FieldAsyncWithScope<BooleanGraphType, bool>(
                "removeUser",
                arguments: 
                new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var command = new RemoveUserFromCommunicationChannelCommand()
                    {
                        UserId = ctx.GetString("userId"),
                        ChannelId = ctx.GetString("channelId")
                    };

                    var channel = await mediator.Send(command);
                    
                    communicationChannelSubscriptionProvider1.UpdateCommunicationChannel(channel);

                    return true;
                }
            );
        }
    }
}