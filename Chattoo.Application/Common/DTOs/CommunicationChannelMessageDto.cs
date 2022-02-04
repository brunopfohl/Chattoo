using AutoMapper;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.Common.DTOs
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
        /// Vrací nebo nastavuje jméno uživatele, který je autorem této zprávy.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ zprávy (bežná zpráva, oznámení, ...).
        /// </summary>
        public CommunicationChannelMessageType Type { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CommunicationChannelMessage, CommunicationChannelMessageDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.UserName));
        }
    }
}