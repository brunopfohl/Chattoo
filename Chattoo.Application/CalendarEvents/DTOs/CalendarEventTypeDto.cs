using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Application.CalendarEvents.DTOs
{
    /// <summary>
    /// Typ kalendářní události.
    /// </summary>
    public class CalendarEventTypeDto : AuditableDto, IMapFrom<CalendarEventType>, ICalendarEventType
    {
        /// <summary>
        /// Vrací nebo nastavuje název typu události.
        /// </summary>
        public string Name { get; set; }
    }
}