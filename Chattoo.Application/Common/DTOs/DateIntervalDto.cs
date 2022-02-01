using System;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Časový interval (od, do).
    /// </summary>
    public class DateIntervalDto : IMapFrom<DateInterval>, IDateInterval
    {
        public DateIntervalDto(DateTime startsAt, DateTime endsAt)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public DateIntervalDto()
        {
            
        }

        /// <summary>
        /// Vrací nebo nastavuje Id čašového bloku.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas, od.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas, kdy.
        /// </summary>
        public DateTime EndsAt { get; set; }
    }
}