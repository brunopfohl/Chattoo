using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření kalendářní události v komunikačním kanálu.
    /// </summary>
    public class CreateCalendarEventCommand : IRequest<CalendarEventDto>
    {
        public DateTime StartsAt { get; set; }
        
        public DateTime? EndsAt { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string CommunicationChannelId { get; set; }
        
        public string GroupId { get; set; }
        
        public string CalendarEventTypeId { get; set; } 
        
        public int? MaximalParticipantsCount { get; set; }
    }

    public class CreateCalendarEventCommandHandler : IRequestHandler<CreateCalendarEventCommand, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventManager _eventManager;
        private readonly ICalendarEventRepository _eventRepository;

        public CreateCalendarEventCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
            ICalendarEventRepository eventRepository, CalendarEventManager eventManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
            _eventManager = eventManager;
        }

        public async Task<CalendarEventDto> Handle(CreateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var calendarEvent = await _eventManager.CreateEvent
            (
                request.CommunicationChannelId,
                request.GroupId,
                request.CalendarEventTypeId,
                request.Name,
                request.Description,
                request.MaximalParticipantsCount,
                request.StartsAt,
                request.EndsAt
            );

            await _eventRepository.AddOrUpdateAsync(calendarEvent, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CalendarEventDto>(calendarEvent);
        }
    }
}