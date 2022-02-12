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
    /// Entita přání o vytvoření události.
    /// </summary>
    public class CalendarEventWish : AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        private List<CalendarEventType> _types;
        private List<DateInterval> _dateIntervals;
        
        protected CalendarEventWish()
        {
            _types = new List<CalendarEventType>();
            _dateIntervals = new List<DateInterval>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int? MinimalParticipantsCount { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora.
        /// </summary>
        public string AuthorId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny lidí, se kterými má uživatel zájem zorganizovat událost.
        /// </summary>
        public string GroupId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id kalendářní události, která obsluhuje toto přání.
        /// </summary>
        public string CalendarEventId { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci typů událostí, o které má uživatel zájem.
        /// </summary>
        public virtual IReadOnlyCollection<CalendarEventType> Types => _types.AsReadOnly();

        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje konání události.
        /// </summary>
        public virtual IReadOnlyCollection<DateInterval> DateIntervals => _dateIntervals.AsReadOnly();

        public void SetMinimalParticipantsCount(int? count)
        {
            if (count.HasValue)
            {
                if (count.Value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinimalParticipantsCount));
                }

                if (MaximalParticipantsCount.HasValue && count.Value > MaximalParticipantsCount.Value)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            MinimalParticipantsCount = count;
        }

        public void SetMaximalParticipantsCount(int? count)
        {
            
        }
        
        public void AddInterval(DateInterval newInterval)
        {
            foreach (var interval in DateIntervals)
            {
                if (newInterval.OverlapsWith(interval))
                {
                    throw new DuplicitDateIntervalPartException(newInterval, interval);
                }
            }
            
            _dateIntervals.Add(newInterval);
        }

        public void RemoveInterval(DateInterval interval)
        {
            bool wasRemoved = _dateIntervals.Remove(interval);

            if (!wasRemoved)
            {
                throw new CalendarEventWishDateIntervalNotFoundException(Id, interval);
            }
        }

        public void AddType(CalendarEventType eventType)
        {
            if (Types.Contains(eventType))
            {
                throw new DuplicitCalendarEventTypeException(Id, eventType.Id);
            }
            
            _types.Add(eventType);
        }
        
        public void RemoveType(CalendarEventType eventType)
        {
            bool wasRemoved = _types.Remove(eventType);
            
            if (!wasRemoved)
            {
                throw new CalendarEventTypeNotFoundException(eventType.Id);
            }
        }
        
        private static CalendarEventWish Create(User user, ICollection<DateInterval> dateIntervals,
            ICollection<CalendarEventType> types, int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            var entity = new CalendarEventWish()
            {
                AuthorId = user.Id
            };
            
        }
        
        public static CalendarEventWish Create(User author, CommunicationChannel channel,
            ICollection<DateInterval> dateIntervals, ICollection<CalendarEventType> types,
            int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            
        }
        
        public static CalendarEventWish Create(User author, CommunicationChannel channel, Group group,
            int? minimalParticipantsCount, int? maximalParticipantsCount,
            ICollection<IDateInterval> intervals, ICollection<ICalendarEventType> types)
        {
            var entity = new CalendarEventWish();
            
            entity.AuthorId = author?.Id;
            entity.CommunicationChannelId = channel?.Id;
            entity.GroupId = group?.Id;
            entity.MinimalParticipantsCount = minimalParticipantsCount;
            entity.MaximalParticipantsCount = maximalParticipantsCount;
            
            return entity;
        }
    }
}