using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEvents.Queries.Get;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.CalendarEventWishes.Queries;
using Chattoo.Application.Common.Models;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CalendarEventWishQuery : ObjectGraphType
    {
        public CalendarEventWishQuery()
        {
            Name = "CalendarEventWishQuery";
            
            this.FieldAsyncWithScope<PageInfoGraphType<CalendarEventWishGraphType, CalendarEventWishDto>, PaginatedList<CalendarEventWishDto>>(
                "getActive",
                arguments: 
                new QueryArgumentsWithPagination(),
                resolve: async (ctx, mediator) =>
                {
                    var query = new GetWishesQuery()
                    {
                        PageNumber = ctx.GetInt("pageNumber"),
                        PageSize = ctx.GetInt("pageSize")
                    };

                    return await mediator.Send(query);
                }
            );
        }
    }
}