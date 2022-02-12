using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitCalendarEventTypeException : Exception
    {
        public DuplicitCalendarEventTypeException(string eventId, string eventTypeId)
        {
            EventId = eventId;
            EventTypeId = eventTypeId;
        }

        public string EventId { get; }
        
        public string EventTypeId { get; }
    }
}