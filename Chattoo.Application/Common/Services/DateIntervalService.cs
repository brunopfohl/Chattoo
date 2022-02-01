using System.Collections.Generic;
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
                for (int j = i; j < intervals.Count; j++)
                {
                    if (intervals[i].GetOverlap(intervals[j]) != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}