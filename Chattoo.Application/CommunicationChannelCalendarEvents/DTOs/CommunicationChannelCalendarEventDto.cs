using System;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.DTOs
{
    /// <summary>
    /// Kalendářní událost z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelCalendarEventDto : AuditableDto, IMapFrom<CommunicationChannelCalendarEvent>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas počátku kalendářní události.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce kalendářní události.
        /// </summary>
        public DateTime? EndsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora kalendářní události.
        /// </summary>
        public string AuthorId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje jméno autora kalendářní události.
        /// </summary>
        public string AuthorName { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název události.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisek události.
        /// </summary>
        public string Description { get; set; }
    }
}