using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita zprávy z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelMessage : AuditableEntity, IAuditableEntity
    {
        private List<CommunicationChannelMessageAttachment> _attachments;

        protected CommunicationChannelMessage()
        {
            _attachments = new List<CommunicationChannelMessageAttachment>();
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
        public virtual IReadOnlyCollection<CommunicationChannelMessageAttachment> Attachments =>
            _attachments.AsReadOnly();

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

        internal CommunicationChannelMessageAttachment AddAttachment(string name, byte[] content,
            CommunicationChannelMessageAttachmentType type)
        {
            var attachment = CommunicationChannelMessageAttachment.Create(Id, name, content, type);

            _attachments.Add(attachment);

            return attachment;
        }
        
        internal CommunicationChannelMessageAttachment DeleteAttachment(string attachmentId)
        {
            var attachment = GetAttachment(attachmentId);

            bool wasRemoved = _attachments.Remove(attachment);

            if (!wasRemoved)
            {
                throw new AttachmentNotFoundException(attachmentId);
            }

            return attachment;
        }
        
        public CommunicationChannelMessageAttachment UpdateAttachment(string attachmentId, string name)
        {
            var attachment = GetAttachment(attachmentId)
                ?? throw new AttachmentNotFoundException(attachmentId);
            
            attachment.SetName(name);

            return attachment;
        }

        public CommunicationChannelMessageAttachment GetAttachment(string attachmentId)
        {
            var attachment = Attachments.FirstOrDefault(a => a.Id == attachmentId);

            return attachment;
        }
    }
}
