using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.CommunicationChannels.DTOs
{
    /// <summary>
    /// Komunikační kanál sloužící pro sdílení obsahu (zpráv) mezi uživateli.
    /// </summary>
    public class CommunicationChannelDto : AuditableDto, IMapFrom<CommunicationChannel>
    {
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }
        
        public ICollection<string> ParticipantIds { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CommunicationChannel, CommunicationChannelDto>()
                 .ForMember(d => 
                     d.ParticipantIds, 
                     opt => opt
                         .MapFrom(s => s.Users.Select(utc => utc.UserId))
                     );
        }
    }
}