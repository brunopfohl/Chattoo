using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    public class CreateCalendarEventWishCommand : IRequest<CreateCalendarEventWishResponse>
    {
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačího kanálu, jehož členové se do události mohou zapojit.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int MinimalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální délku události.
        /// </summary>
        public TimeSpan MinimalLength { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ události.
        /// </summary>
        public CalendarEventType Type { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje vytvoření události.
        /// </summary>
        public ICollection<DateIntervalDto> DateIntervals { get; set; }
    }

    public class CreateCalendarEventWishResponse
    {
        public CalendarEventWishDto CreatedWish { get; set; }
        
        public CalendarEventDto JoinedEvent { get; set; }
    }

    public class CreateCalendarEventWishCommandHandler : IRequestHandler<CreateCalendarEventWishCommand, CreateCalendarEventWishResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventWishManager _wishManager;
        private readonly ICalendarEventWishRepository _wishRepository;
        private readonly EventSuggestionService _eventSuggestionService;
        private readonly IMapper _mapper;

        public CreateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupManager groupManager, ChannelManager channelManager, CalendarEventWishManager wishManager, ICalendarEventWishRepository wishRepository, EventSuggestionService eventSuggestionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wishManager = wishManager;
            _wishRepository = wishRepository;
            _eventSuggestionService = eventSuggestionService;
        }

        public async Task<CreateCalendarEventWishResponse> Handle(CreateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishManager.Create
            (
                request.CommunicationChannelId,
                request.Name,
                request.DateIntervals.Cast<IDateInterval>().ToList(),
                request.Type,
                request.MinimalParticipantsCount,
                request.MinimalLength
            );

            await _wishRepository.AddOrUpdateAsync(wish, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = new CreateCalendarEventWishResponse()
            {
                CreatedWish = _mapper.Map<CalendarEventWishDto>(wish)
            };
            
            // Pokusím se vyhovět přání uživatele a zařadit ho do události.
            var suggestionResult = await _eventSuggestionService.OnNewWishAdded(result.CreatedWish);

            if (suggestionResult is not null)
            {
                result.JoinedEvent = suggestionResult.CalendarEvent;
            }

            return result;
        }
    }
}