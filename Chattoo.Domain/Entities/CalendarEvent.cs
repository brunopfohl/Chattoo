using System;
using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Interfaces.CalendarEvent;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita události.
    /// </summary>
    public class CalendarEvent: AuditableEntity, IAuditableEntity, IAggregateRoot,
        IWithRestrictedReadPermissions, IWithRestrictedWritePermissions
    {
        protected CalendarEvent()
        {
            Participants = new List<UserToCalendarEvent>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas začátku události.
        /// </summary>
        public DateTime StartsAt { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce události.
        /// </summary>
        public DateTime? EndsAt { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název události.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisek události.
        /// </summary>
        public string Description { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny.
        /// </summary>
        public string GroupId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora.
        /// </summary>
        public string AuthorId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje adresu.
        /// </summary>
        public Address Address { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id typu události.
        /// </summary>
        public string CalendarEventTypeId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje autora (null reprezentuje "jakýkoliv typ").
        /// </summary>
        public virtual CalendarEventType CalendarEventType { get; private set; }
        
        public virtual ICollection<UserToCalendarEvent> Participants { get; private set; }

        #region IWithRestrictedReadPermissions

        ICollection<string> IWithRestrictedReadPermissions.UsersIds =>
            Participants.Select(p => p.UserId).ToList();
        
        #endregion
        
        #region IWithRestrictedWritePermissions

        string IWithRestrictedWritePermissions.UserId => AuthorId;
        
        #endregion
        
        public static CalendarEvent Create(ICalendarEventCreateContract createContract, User author, CommunicationChannel channel,
            Group group, Address address, CalendarEventType type)
        {
            var entity = new CalendarEvent();
            entity.StartsAt = createContract.StartsAt;
            entity.EndsAt = createContract.EndsAt;
            entity.Name = createContract.Name;
            entity.Description = createContract.Description;
            entity.MaximalParticipantsCount = createContract.MaximalParticipantsCount;

            entity.AuthorId = author?.Id;
            
            entity.CommunicationChannelId = channel?.Id;
            
            entity.GroupId = group?.Id;

            // entity.Address = address;

            entity.CalendarEventType = type;
            entity.CalendarEventTypeId = type?.Id;

            return entity;
        }
    }
}