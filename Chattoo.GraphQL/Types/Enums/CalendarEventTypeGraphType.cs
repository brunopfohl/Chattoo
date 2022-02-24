using Chattoo.Domain.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types.Enums
{
    public class CalendarEventTypeGraphType : EnumerationGraphType<CalendarEventType>
    {
        public CalendarEventTypeGraphType()
        {
            Name = "CalendarEventTypeGraphType";
        }
    }
}