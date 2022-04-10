using Chattoo.Application.CalendarEventWishes.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class CalendarEventWishGraphType : AuditableObjectGraphType<CalendarEventWishDto>
    {
        public CalendarEventWishGraphType()
        {
            Field(o => o.AuthorId);
            Field(o => o.AuthorName);
            Field(o => o.DateIntervals, type: typeof(ListGraphType<DateIntervalGraphType>));
            Field(o => o.MaximalParticipantsCount, type: typeof(IntGraphType), nullable: true);
            Field(o => o.MinimalParticipantsCount, type: typeof(IntGraphType), nullable: true);
        }
    }
}