using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Příloha pro zprávu z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelMessageAttachmentDto : AuditableDto, IMapFrom<CommunicationChannelMessageAttachment>
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
        /// Vrací nebo nastavuje typ přílohy (soubor, odkaz, ...).
        /// </summary>
        public CommunicationChannelMessageAttachmentType Type { get; set; }
    }
}