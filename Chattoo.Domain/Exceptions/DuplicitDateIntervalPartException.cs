using System;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitDateIntervalPartException : Exception
    {
        public DuplicitDateIntervalPartException(DateInterval firstInterval, DateInterval secondInterval)
        {
            FirstInterval = firstInterval;
            SecondInterval = secondInterval;
        }

        public DateInterval FirstInterval { get; }
        
        public DateInterval SecondInterval { get; }
    }
}