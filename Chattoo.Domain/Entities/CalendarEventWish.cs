using System;
using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
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
        private List<DateInterval> _dateIntervals;
        
        protected CalendarEventWish()
        {
            _dateIntervals = new List<DateInterval>();
        }
        
        public string Name { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int MinimalParticipantsCount { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální délku události v minutách.
        /// </summary>
        public long MinimalLengthInMinutes { get; private set; }
        
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
        public CalendarEventType Type { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje konání události.
        /// </summary>
        public virtual IReadOnlyCollection<DateInterval> DateIntervals => _dateIntervals.AsReadOnly();

        /// <summary>
        /// Vrací minimální délku události.
        /// </summary>
        public TimeSpan MinimalLength => TimeSpan.FromMinutes(MinimalLengthInMinutes);

        public void SetMinimalParticipantsCount(int count)
        {
            if (count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(MinimalParticipantsCount));
            }

            MinimalParticipantsCount = count;
        }

        public void SetType(CalendarEventType type)
        {
            Type = type;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        
        public void SetMinimalLength(TimeSpan minimalLength)
        {
            foreach (var dateInterval in DateIntervals)
            {
                CheckDateIntervalWithMinimalLength(MinimalLength, dateInterval);
            }
            
            MinimalLengthInMinutes = (long)minimalLength.TotalMinutes;
        }
        
        public void AddInterval(DateInterval newInterval)
        {
            CheckDateIntervalWithMinimalLength(MinimalLength, newInterval);
                
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

        private static CalendarEventWish Create(User user, string name, ICollection<DateInterval> dateIntervals,
            CalendarEventType type, int minimalParticipantsCount, TimeSpan minimalLength)
        {
            var entity = new CalendarEventWish()
            {
                AuthorId = user.Id,
                Type = type,
                Name = name
            };

            entity.SetMinimalParticipantsCount(minimalParticipantsCount);
            entity.SetMinimalLength(minimalLength);
            
            foreach (var interval in dateIntervals)
            {
                entity.AddInterval(interval);
            }

            return entity;
        }
        
        public static CalendarEventWish Create(User author, string name, CommunicationChannel channel,
            ICollection<DateInterval> dateIntervals, CalendarEventType type,
            int minimalParticipantsCount, TimeSpan minimalLength)
        {
            var entity = Create(author, name, dateIntervals, type, minimalParticipantsCount, minimalLength);

            entity.CommunicationChannelId = channel.Id;

            return entity;
        }

        private void CheckDateIntervalWithMinimalLength(TimeSpan minimalLength, DateInterval dateInterval)
        {
            if (dateInterval.Length < minimalLength)
            {
                throw new DateIntervalTooShortException(dateInterval, Id);
            }
        }
    }
}