using System;

namespace Chattoo.Domain.Exceptions
{
    public class CalendarEventNotFoundException : Exception
    {
        public CalendarEventNotFoundException(string eventId)
        {
            EventId = eventId;
        }

        public string EventId { get; }
    }
}