using System;

namespace Chattoo.Domain.Exceptions
{
    public class CalendarEventCapacityInsufficientException : Exception
    {
        public CalendarEventCapacityInsufficientException(string eventId, int capacity, int wantedCapacity)
        {
            EventId = eventId;
            Capacity = capacity;
            WantedCapacity = wantedCapacity;
        }

        public string EventId { get; }
        
        public int Capacity { get; }
        
        public int WantedCapacity { get; }
    }
}