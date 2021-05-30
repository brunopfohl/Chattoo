using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.CommunicationChannelCalendarEvents.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Queries.GetById
{
    /// <summary>
    /// Dotaz na kalendářní události s daným Id.
    /// </summary>
    public class GetCommunicationChannelCalendarEventByIdQuery : IRequest<CommunicationChannelCalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id události z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetCommunicationChannelCalendarEventByIdQueryHandler : IRequestHandler<GetCommunicationChannelCalendarEventByIdQuery, CommunicationChannelCalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelCalendarEventRepository _communicationChannelCalendarEventRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCommunicationChannelCalendarEventByIdQueryHandler(IMapper mapper, ICommunicationChannelCalendarEventRepository communicationChannelCalendarEventRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _communicationChannelCalendarEventRepository = communicationChannelCalendarEventRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CommunicationChannelCalendarEventDto> Handle(GetCommunicationChannelCalendarEventByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu událost z komunikačního kanálu z datového zdroje.
            var calendarEvent = await _communicationChannelCalendarEventRepository.GetByIdAsync(request.Id)
                                ?? throw new NotFoundException(nameof(CommunicationChannelCalendarEvent), request.Id);

            // Pokud aktuálně přihlášený uživatel nemá právo na zobrazení události, vyhodím výjimku.
            if (!calendarEvent.CommunicationChannel.Users.Contains(_currentUserService.User))
            {
                throw new FormatException();
            }

            return _mapper.Map<CommunicationChannelCalendarEventDto>(calendarEvent);
        }
    }
}