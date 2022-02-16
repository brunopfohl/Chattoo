using System;

namespace Chattoo.Domain.Exceptions
{
    public class CalendarEventWishNotFoundException : Exception
    {
        public CalendarEventWishNotFoundException(string wishId)
        {
            WishId = wishId;
        }
        
        public string WishId { get; set; }
    }
}