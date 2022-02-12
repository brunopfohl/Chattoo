using System;

namespace Chattoo.Domain.Exceptions
{
    public class CalendarEventTypeNotFoundException : Exception
    {
        public CalendarEventTypeNotFoundException(string eventTypeId)
        {
            EventTypeId = eventTypeId;
        }

        public string EventTypeId { get; }
    }
}