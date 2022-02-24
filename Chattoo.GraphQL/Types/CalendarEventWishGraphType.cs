using Chattoo.Application.CalendarEventWishes.DTOs;

namespace Chattoo.GraphQL.Types
{
    public class CalendarEventWishGraphType : AuditableObjectGraphType<CalendarEventWishDto>
    {
        public CalendarEventWishGraphType()
        {
            Field(o => o.MaximalParticipantsCount);
            Field(o => o.MinimalParticipantsCount);
        }
    }
}