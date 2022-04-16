using System;
using System.Collections.Generic;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.CalendarEventWishes.DTOs
{
    public class CalendarEventWishDto : AuditableDto, IMapFrom<CalendarEventWish>
    {
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int MinimalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální délku události.
        /// </summary>
        public TimeSpan MinimalLength { get; set; }

        /// <summary>
        /// Vrací minimální délku události v minutách.
        /// </summary>
        public long MinimalLengthInMinutes => (long)MinimalLength.TotalMinutes;
        
        /// <summary>
        /// Vrací nebo nastavuje Id autora.
        /// </summary>
        public string AuthorId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje seznam časových bloků, kterých se přání týká.
        /// </summary>
        public List<DateIntervalDto> DateIntervals { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalendarEventWish, CalendarEventWishDto>();
        }
    }
}