using System;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita události v komunikačním kanálu.
    /// </summary>
    public class CommunicationChannelCalendarEvent : AuditableEntity, IAuditableEntity
    {
        /// <summary>
        /// Vrací nebo nastavuje datum a čas začátku události.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce události.
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
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje komunikační kanál.
        /// </summary>
        public virtual CommunicationChannel CommunicationChannel { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje autora.
        /// </summary>
        public virtual User User { get; set; }
    }
}