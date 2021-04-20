using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita zprávy z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelMessage : AuditableEntity, IAuditableEntity
    {
        public CommunicationChannelMessage()
        {
            Attachments = new List<CommunicationChannelMessageAttachment>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, které ho je tato zpráva součástí.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který je autorem této zprávy.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje typ zprávy (bežná zpráva, oznámení, ...).
        /// </summary>
        public CommunicationChannelMessageType Type { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje komunikační kanál, kterého je tato zpráva součástí.
        /// </summary>
        public virtual CommunicationChannel Channel { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje uživatele, který je autorem této zprávy.
        /// </summary>
        public virtual User User { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci příloh, které jsou připnuty k této zprávě.
        /// </summary>
        public virtual ICollection<CommunicationChannelMessageAttachment> Attachments { get; set; }
    }
}
