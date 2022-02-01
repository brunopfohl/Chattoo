using System.Collections.Generic;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.CalendarEventWishes.DTOs
{
    public class CalendarEventWishDto : AuditableDto, IMapFrom<CalendarEventWish>
    {
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int? MinimalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora.
        /// </summary>
        public string AuthorId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje jméno autora.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje seznam časových bloků, kterých se přání týká.
        /// </summary>
        public List<DateIntervalDto> DateIntervals { get; set; }
        
        public void Mapping(Profile profile)
        {
            // profile.CreateMap<CalendarEventWish, CalendarEventWishDto>()
            //     .ForMember(d => 
            //         d.AuthorName, 
            //         opt => opt.MapFrom(s => s.Author.UserName)
            //         );
        }
    }
}