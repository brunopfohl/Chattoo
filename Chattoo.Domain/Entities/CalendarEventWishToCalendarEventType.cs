using System.Collections.Generic;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.Entities
{
    public class CalendarEventWishToCalendarEventType : ValueObject
    {
        protected CalendarEventWishToCalendarEventType()
        {
            
        }
        
        public string WishId { get; set; }
        
        public string TypeId { get; set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return WishId;
            yield return TypeId;
        }
    }
}