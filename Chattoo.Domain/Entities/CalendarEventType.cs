using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita reprezentující typ kalendářní události.
    /// </summary>
    public class CalendarEventType : AuditableEntity, IAuditableEntity
    {
        protected CalendarEventType()
        {
            
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název typu události.
        /// </summary>
        public string Name { get; private set; }
    }
}