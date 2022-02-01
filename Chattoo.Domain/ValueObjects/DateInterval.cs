using System;
using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.ValueObjects
{
    /// <summary>
    /// Časový interval.
    /// </summary>
    public class DateInterval : ValueObject, IDateInterval
    {
        protected DateInterval()
        {
            
        }
        
        protected DateInterval(DateTime startsAt, DateTime endsAt)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        /// <summary>
        /// Vytvoří novou instanci <see cref="DateInterval"/> hodnotového objektu.
        /// </summary>
        /// <param name="startsAt">Počátek intervalu</param>
        /// <param name="endsAt">Konec intervalu</param>
        /// <exception cref="ArgumentOutOfRangeException">Výjimka - interval končí dříve než začíná.</exception>
        public static DateInterval Create(DateTime startsAt, DateTime endsAt)
        {
            if (startsAt > endsAt)
            {
                throw new ArgumentOutOfRangeException($"DateInterval {startsAt} - {endsAt} is not valid.");
            }

            return new DateInterval(startsAt, endsAt);
        }

        /// Vytvoří novou instanci <see cref="DateInterval"/> hodnotového objektu vytvořeného z <see cref="IDateInterval"/>.
        /// </summary>
        /// <param name="interval">Interval</param>
        /// <exception cref="ArgumentOutOfRangeException">Výjimka - interval končí dříve než začíná.</exception>
        public static DateInterval Create(IDateInterval interval)
        {
            return Create(interval.StartsAt, interval.EndsAt);
        }

        public DateTime StartsAt { get; }
        
        public DateTime EndsAt { get; }
        
        /// <summary>
        /// Vrací překrytí (časový interval) mezi 2 časovými intervaly.
        /// </summary>
        /// <param name="src">První časový interval</param>
        /// <param name="interval">Druhý časový interval</param>
        public DateInterval GetOverlap(DateInterval interval)
        {
            // Intervaly mají průnik, pokud:
            // - 2. interval končí později než 1. začíná A
            // - 1. interval končí později než 2. začíná
            if (StartsAt <= interval.EndsAt && interval.StartsAt <= EndsAt)
            {
                // Počátek intervalu maximaluzuji.
                DateTime startsAt = StartsAt > interval.StartsAt
                    ? StartsAt
                    : interval.StartsAt;
                
                // Konec intervalu minimalizuji.
                DateTime endsAt = EndsAt < interval.EndsAt
                    ? EndsAt
                    : interval.EndsAt;

                return new DateInterval(startsAt, endsAt);
            }

            return null;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartsAt;
            yield return EndsAt;
        }
    }
}