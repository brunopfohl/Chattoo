using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita přílohy pro zprávu z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelMessageAttachment : AuditableEntity, IAuditableEntity
    {
        /// <summary>
        /// Vrací nebo nastavuje název této přílohy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje obsah této přílohy.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje Id zprávy, ke které je tato příloha připnuta.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje zprávu, ke které je tato příloha připnuta.
        /// </summary>
        public virtual CommunicationChannelMessage Message { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje typ přílohy (soubor, odkaz, ...).
        /// </summary>
        public CommunicationChannelMessageAttachmentType Type { get; set; }
    }
}
