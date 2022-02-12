using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicateUserInCalendarEventException : Exception
    {
        public DuplicateUserInCalendarEventException(string eventId, string userId)
        {
            EventId = eventId;
            UserId = userId;
        }

        public string EventId { get; }
        
        public string UserId { get; }
    }
}