using System;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Exceptions
{
    public class DateIntervalTooShortException : Exception
    {
        public DateIntervalTooShortException(IDateInterval dateInterval, string wishId)
        {
            WishId = wishId;
            DateInterval = dateInterval;
        }

        public IDateInterval DateInterval { get; }
        
        public string WishId { get; }
    }
}