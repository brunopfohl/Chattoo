using System;
using System.Linq;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.CalendarEvents.DTOs
{
    /// <summary>
    /// Kalendářní událost z komunikačního kanálu.
    /// </summary>
    public class CalendarEventDto : AuditableDto, IMapFrom<CalendarEvent>
    {
        /// <summary>
        /// Vrací nebo nastavuje datum a čas počátku kalendářní události.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce kalendářní události.
        /// </summary>
        public DateTime? EndsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název události.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisek události.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora kalendářní události.
        /// </summary>
        public string AuthorId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ kalendářní události.
        /// </summary>
        public CalendarEventType Type { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje počet účastníků.
        /// </summary>
        public int ParticipantsCount { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalendarEvent, CalendarEventDto>()
                .ForMember(d => 
                    d.ParticipantsCount, 
            opt => opt
                            .MapFrom(e => e.Participants.Count())
            );
        }
    }
}