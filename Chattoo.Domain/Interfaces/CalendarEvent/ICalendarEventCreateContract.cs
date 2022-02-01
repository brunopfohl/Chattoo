using System;

namespace Chattoo.Domain.Interfaces.CalendarEvent
{
    public interface ICalendarEventCreateContract
    {
        /// <summary>
        /// Vrací nebo nastavuje datum a čas počátku kalendářní události.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce kalendářní události.
        /// </summary>
        public DateTime? EndsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název události.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisek události.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id typu události.
        /// </summary>
        public string CalendarEventTypeId { get; set; }
    }
}