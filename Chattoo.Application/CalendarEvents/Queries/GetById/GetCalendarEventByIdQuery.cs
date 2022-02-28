using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
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

            var result = _mapper.Map<CalendarEventDto>(calendarEvent);
            return result;
        }
    }
}