using Chattoo.Application.CalendarEvents.DTOs;

namespace Chattoo.GraphQL.Types
{
    public class CalendarEventTypeGraphType : AuditableObjectGraphType<CalendarEventTypeDto>
    {
        public CalendarEventTypeGraphType()
        {
            Name = "CalendarEventType";

            Field(o => o.Name);
        }
    }
}