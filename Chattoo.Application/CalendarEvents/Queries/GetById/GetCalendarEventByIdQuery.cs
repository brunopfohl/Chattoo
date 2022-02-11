using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Queries
{
    /// <summary>
    /// Dotaz na kalendářní události s daným Id.
    /// </summary>
    public class GetCalendarEventByIdQuery : IRequest<CalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id události z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetCalendarEventByIdQueryHandler : IRequestHandler<GetCalendarEventByIdQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCalendarEventByIdQueryHandler(IMapper mapper, ICalendarEventRepository calendarEventRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _calendarEventRepository = calendarEventRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CalendarEventDto> Handle(GetCalendarEventByIdQuery request, CancellationToken cancellationToken)
        {
            // // Načtu událost z komunikačního kanálu z datového zdroje.
            // var calendarEvent = await _calendarEventRepository.GetByIdAsync(request.Id)
            //                     ?? throw new NotFoundException(nameof(CalendarEvent), request.Id);
            //
            // // Pokud aktuálně přihlášený uživatel nemá právo na zobrazení události, vyhodím výjimku.
            // if (!calendarEvent.CommunicationChannel.Users.Contains(_currentUserService.User))
            // {
            //     throw new FormatException();
            // }

            //return _mapper.Map<CalendarEventDto>(calendarEvent);
            return _mapper.Map<CalendarEventDto>(null);
        }
    }
}