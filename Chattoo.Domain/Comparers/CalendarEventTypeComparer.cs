using System.Collections.Generic;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Comparers
{
    public class CalendarEventTypeComparer : IEqualityComparer<CalendarEventType>
    {
        public bool Equals(CalendarEventType x, CalendarEventType y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(CalendarEventType obj)
        {
            return (obj.Id != null ? obj.Id.GetHashCode() : 0);
        }
    }
}