using System.Collections.Generic;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.Entities
{
    public class UserToCalendarEvent : ValueObject
    {
        protected UserToCalendarEvent()
        {
            
        }
        
        public string UserId { get; private set; } 
        
        public string EventId { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return EventId;
        }
    }
}