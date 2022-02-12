using System;
using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita události.
    /// </summary>
    public class CalendarEvent: AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        private List<UserToCalendarEvent> _participants;
        
        protected CalendarEvent()
        {
            _participants = new List<UserToCalendarEvent>();
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

        public virtual IReadOnlyCollection<UserToCalendarEvent> Participants => _participants.AsReadOnly();

        public bool HasParticipant(string userId)
        {
            return Participants.Any(u => u.UserId == userId);
        }

        internal void AddParticipant(string userId)
        {
            if (HasParticipant(userId))
            {
                throw new DuplicateUserInCalendarEventException(Id, userId);
            }

            var participant = UserToCalendarEvent.Create(userId, Id);
            
            _participants.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant =_participants.FirstOrDefault(p => p.UserId == participantId)
                ?? throw new UserNotFoundException(participantId);

            _participants.Remove(participant);
        }

        internal void SetMaximalParticipantsCount(int? count)
        {
            if (count.HasValue)
            {
                if (count.Value < 1)
                {
                    throw new ArgumentOutOfRangeException("Maximální počet účastníků musí být neomezený nebo vyšší než 0");
                }

                if (_participants.Count > count)
                {
                    throw new CalendarEventCapacityInsufficientException(Id, _participants.Count, count.Value);
                }
            }

            MaximalParticipantsCount = count;
        }
        
        internal void SetStartsAt(DateTime startsAt)
        {
            if (EndsAt.HasValue && startsAt > EndsAt.Value)
            {
                ThrowInvalidTimeRangeException();
            }

            StartsAt = startsAt;
        }
        
        internal void SetEndsAt(DateTime? endsAt)
        {
            if (endsAt.HasValue && StartsAt > endsAt.Value)
            {
                ThrowInvalidTimeRangeException();
            }

            EndsAt = endsAt;
        }

        internal void SetName(string name)
        {
            Name = name;
        }

        internal void SetDescription(string description)
        {
            Description = description;
        }

        internal void SetAddress(Address address)
        {
            Address = address;
        }

        private static CalendarEvent Create(User author, CalendarEventType type, string name, string description)
        {
            var entity = new CalendarEvent()
            {
                AuthorId = author.Id,
                CalendarEventType = type,
                CalendarEventTypeId = type?.Id
            };
            
            entity.SetName(name);
            entity.SetDescription(description);

            return entity;
        }
        
        public static CalendarEvent Create(User author, CommunicationChannel channel, CalendarEventType type,
            string name, string description)
        {
            var entity = Create(author, type, name, description);

            entity.CommunicationChannelId = channel.Id;

            return entity;
        }
        
        public static CalendarEvent Create(User author, Group group, CalendarEventType type,
            string name, string description)
        {
            var entity = Create(author, type, name, description);

            entity.GroupId = group.Id;

            return entity;
        }

        private void ThrowInvalidTimeRangeException()
        {
            throw new ArgumentOutOfRangeException("Čas 'od' nemůže být později než čas 'do'.");
        }
    }
}