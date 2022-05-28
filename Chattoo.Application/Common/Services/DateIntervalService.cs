using System;
using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Application.Common.Services
{
    /// <summary>
    /// Služba pro práci s časovými intervaly.
    /// </summary>
    public class DateIntervalService
    {
        public DateIntervalService()
        {
            
        }
        
        /// <summary>
        /// Vrací, zda-li kolekce předaných intervalů obsahuje alespoň dva intervaly, které se překrývají.
        /// </summary>
        /// <param name="intervals">Kolekce časových intervalů.</param>
        public bool GetOverlapOfIntervals(List<DateInterval> intervals)
        {
            for (int i = 0; i < intervals.Count; i++)
            {
                for (int j = i + 1; j < intervals.Count; j++)
                {
                    if (intervals[i].GetOverlap(intervals[j]) != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public List<DateInterval> GetOverlaps(IEnumerable<DateInterval> a, IEnumerable<DateInterval> b, TimeSpan minimalLength)
        {
            var result = new List<DateInterval>();

            var aSorted = a.OrderBy(aI => aI.StartsAt);
            var bSorted = b.OrderBy(bI => bI.StartsAt).ToList();

            foreach (var aI in aSorted)
            {
                foreach (var bI in bSorted)
                {
                    if (bI.StartsAt > aI.EndsAt)
                        break;

                    var overlap = bI.GetOverlap(aI);

                    if (overlap is not null && overlap.Length >= minimalLength)
                    {
                        result.Add(overlap);
                    }
                }
            }

            return result;
        }
    }
}