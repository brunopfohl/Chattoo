using System;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Exceptions
{
    public class CalendarEventWishDateIntervalNotFoundException : Exception
    {
        public CalendarEventWishDateIntervalNotFoundException(string wishId, DateInterval dateInterval)
        {
            WishId = wishId;
            DateInterval = dateInterval;
        }

        public string WishId { get; }
        
        public DateInterval DateInterval { get; }
    }
}