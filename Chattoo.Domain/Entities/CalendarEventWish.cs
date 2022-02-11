using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita přání o vytvoření události.
    /// </summary>
    public class CalendarEventWish : AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        protected CalendarEventWish()
        {
            Types = new List<CalendarEventType>();
            DateIntervals = new List<DateInterval>();
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
        public virtual ICollection<CalendarEventType> Types { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje konání události.
        /// </summary>
        public virtual ICollection<DateInterval> DateIntervals { get; private set; }

        /// <summary>
        /// Vytvoří novou instanci entity <see cref="CalendarEventWish"/>.
        /// </summary>
        /// <param name="author">Entita uživatele, který přání vytvořil.</param>
        /// <param name="channel">Entita komunikačního kanálu.</param>
        /// <param name="group">Entita skupiny.</param>
        /// <param name="minimalParticipantsCount">Minimální počet uživatelů, který musí mít o událost zájem, aby vznikla.</param>
        /// <param name="maximalParticipantsCount">Maximální počet uživatelů, který musí mít o událost zíjem, aby vznikla.</param>
        /// <param name="intervals">Časové bloky, kdy uživatel má čas na událost.</param>
        /// <param name="types">Tipy aktivit, které si uživatel přeje.</param>
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