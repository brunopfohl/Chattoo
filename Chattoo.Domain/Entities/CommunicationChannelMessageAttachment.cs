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
        protected CommunicationChannelMessageAttachment()
        {
            
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název této přílohy.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje obsah této přílohy.
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje Id zprávy, ke které je tato příloha připnuta.
        /// </summary>
        public string MessageId { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje typ přílohy (soubor, odkaz, ...).
        /// </summary>
        public CommunicationChannelMessageAttachmentType Type { get; private set; }

        public static CommunicationChannelMessageAttachment Create(string messageId, string name,
            byte[] content, CommunicationChannelMessageAttachmentType type)
        {
            var entity = new CommunicationChannelMessageAttachment()
            {
                MessageId = messageId,
                Content = content,
                Type = type
            };

            entity.SetName(name);

            return entity;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}