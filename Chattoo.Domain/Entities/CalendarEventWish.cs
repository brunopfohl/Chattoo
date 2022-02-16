using System;
using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Comparers;
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
                    throw new ArgumentOutOfRangeException(
                        nameof(MinimalParticipantsCount), 
                        $"{nameof(MaximalParticipantsCount)} has to be same or higher than {nameof(MinimalParticipantsCount)}."
                    );
                }
            }

            MinimalParticipantsCount = count;
        }

        public void SetMaximalParticipantsCount(int? count)
        {
            if (count.HasValue && count.Value < MinimalParticipantsCount)
            {
                throw new ArgumentOutOfRangeException();
            }

            MaximalParticipantsCount = count;
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

        public void UpdateDateIntervals(ICollection<DateInterval> dateIntervals)
        {
            _dateIntervals.Clear();

            foreach (var dateInterval in dateIntervals)
            {
                AddInterval(dateInterval);
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

        public void UpdateTypes(ICollection<CalendarEventType> eventTypes)
        {
            var set = new HashSet<CalendarEventType>(new CalendarEventTypeComparer());

            foreach (var type in eventTypes)
            {
                set.Add(type);
            }

            var removedTypes = new HashSet<CalendarEventType>();
            var addedTypes = new HashSet<CalendarEventType>();
            
            foreach (var type in Types)
            {
                if (set.Add(type))
                {
                    removedTypes.Add(type);
                }
                else
                {
                    addedTypes.Add(type);
                }
            }

            foreach (var toRemove in removedTypes)
            {
                _types.Remove(toRemove);
            }
            
            foreach (var toAdd in addedTypes)
            {
                _types.Remove(toAdd);
            }
        }

        private static CalendarEventWish Create(User user, ICollection<DateInterval> dateIntervals,
            ICollection<CalendarEventType> types, int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            var entity = new CalendarEventWish()
            {
                AuthorId = user.Id
            };

            foreach (var interval in dateIntervals)
            {
                entity.AddInterval(interval);
            }

            foreach (var type in types)
            {
                entity.AddType(type);
            }
            
            entity.SetMinimalParticipantsCount(minimalParticipantsCount);
            
            entity.SetMaximalParticipantsCount(maximalParticipantsCount);

            return entity;
        }
        
        public static CalendarEventWish Create(User author, CommunicationChannel channel,
            ICollection<DateInterval> dateIntervals, ICollection<CalendarEventType> types,
            int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            var entity = CalendarEventWish.Create(author, dateIntervals, types, minimalParticipantsCount,
                maximalParticipantsCount);

            entity.CommunicationChannelId = channel.Id;

            return entity;
        }
        
        public static CalendarEventWish Create(User author, Group group,
            ICollection<DateInterval> dateIntervals, ICollection<CalendarEventType> types,
            int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            var entity = CalendarEventWish.Create(author, dateIntervals, types, minimalParticipantsCount,
                maximalParticipantsCount);

            entity.GroupId = group.Id;

            return entity;
        }
    }
}