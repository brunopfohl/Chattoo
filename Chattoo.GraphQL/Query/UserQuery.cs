using System.Collections.Generic;
using Chattoo.Application.CalendarEvents.Queries;
using Chattoo.Application.Common.Models;
using Chattoo.Application.CommunicationChannels.Queries.GetUsers;
using Chattoo.Application.Groups.Queries;
using Chattoo.Application.Users.DTOs;
using Chattoo.Application.Users.Queries;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery()
        {
            Name = "UserQuery";
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserGraphType, UserDto>, PaginatedList<UserDto>>(
                "getForCommunicationChannel",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "channelId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersForChannelQuery()
                    {
                        ChannelId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserGraphType, UserDto>, PaginatedList<UserDto>>(
                "getForGroup",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersForGroupQuery()
                    {
                        GroupId = ctx.GetString("channelId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserGraphType, UserDto>, PaginatedList<UserDto>>(
                "getForCalendarEvent",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "calendarEventId" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersForCalendarEventQuery()
                    {
                        EventId = ctx.GetString("calendarEventId"),
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
            
            this.FieldAsyncWithScope<PageInfoGraphType<UserGraphType, UserDto>, PaginatedList<UserDto>>(
                "get",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "searchTerm" },
                    new QueryArgument<ListGraphType<StringGraphType>> { Name = "excludedUserIds" }
                ),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetUsersQuery()
                    {
                        SearchTerm = ctx.GetString("searchTerm"),
                        ExcludedUserIds = ctx.GetArgument<List<string>>("excludedUserIds")
                    };

                    var result = await mediator.Send(query);
                    return result;
                }
            );
        }
    }
}