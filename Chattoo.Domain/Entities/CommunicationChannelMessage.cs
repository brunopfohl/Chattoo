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
        protected CommunicationChannelMessage()
        {
            Attachments = new List<CommunicationChannelMessageAttachment>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, které ho je tato zpráva součástí.
        /// </summary>
        public string ChannelId { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který je autorem této zprávy.
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje typ zprávy (bežná zpráva, oznámení, ...).
        /// </summary>
        public CommunicationChannelMessageType Type { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje uživatele, který je autorem této zprávy.
        /// </summary>
        public virtual User User { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci příloh, které jsou připnuty k této zprávě.
        /// </summary>
        public virtual ICollection<CommunicationChannelMessageAttachment> Attachments { get; private set; }

        public void SetContent(string content)
        {
            Content = content;
        }
        
        public static CommunicationChannelMessage Create(string userId, string channelId,
            string content, CommunicationChannelMessageType type)
        {
            var entity = new CommunicationChannelMessage()
            {
                UserId = userId,
                ChannelId = channelId,
                Content = content,
                Type = type
            };

            return entity;
        }
    }
}
