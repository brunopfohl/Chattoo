using Chattoo.Application.CalendarEventWishes.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class CalendarEventWishGraphType : AuditableObjectGraphType<CalendarEventWishDto>
    {
        public CalendarEventWishGraphType()
        {
            Field(o => o.Name);
            Field(o => o.DateIntervals, type: typeof(ListGraphType<DateIntervalGraphType>));
            Field(o => o.MinimalParticipantsCount, type: typeof(IntGraphType), nullable: true);
            Field(o => o.MinimalLengthInMinutes, type: typeof(LongGraphType), nullable: true);
        }
    }
}