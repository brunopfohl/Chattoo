using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.CommunicationChannelMessages.DTOs
{
    /// <summary>
    /// Zpráva z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelMessageDto : AuditableDto, IMapFrom<CommunicationChannelMessage>
    {
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
    }
}