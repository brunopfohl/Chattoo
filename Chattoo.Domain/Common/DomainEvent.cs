using System;

namespace Chattoo.Domain.Common
{

    /// <summary>
    /// Událost, která se děje doménovému objektu.
    /// </summary>
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
