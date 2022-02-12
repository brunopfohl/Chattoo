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
using Chattoo.Domain.Services;
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
        private readonly CalendarEventManager _eventManager;

        public GetCalendarEventByIdQueryHandler(IMapper mapper, CalendarEventManager eventManager)
        {
            _mapper = mapper;
            _eventManager = eventManager;
        }

        public async Task<CalendarEventDto> Handle(GetCalendarEventByIdQuery request, CancellationToken cancellationToken)
        {
            var calendarEvent = await _eventManager.GetEventOrThrow(request.Id);

            return _mapper.Map<CalendarEventDto>(calendarEvent);
        }
    }
}