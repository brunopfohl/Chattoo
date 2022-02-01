using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Interfaces.CalendarEvent;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření kalendářní události v komunikačním kanálu.
    /// </summary>
    public class CreateCalendarEventCommand : IRequest<CalendarEventDto>, ICalendarEventCreateContract
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
        private readonly ICurrentUserService _currentUserService;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public CreateCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICalendarEventRepository calendarEventRepository,
            ICurrentUserService currentUserService, IMapper mapper, ICommunicationChannelRepository communicationChannelRepository,
            IGroupRepository groupRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _calendarEventRepository = calendarEventRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _groupRepository = groupRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<CalendarEventDto> Handle(CreateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // CommunicationChannel channel = await _getByIdUserSafeService
            //         .GetAsync(_communicationChannelRepository, request.CommunicationChannelId);
            //
            // Group group = await _getByIdUserSafeService
            //         .GetAsync(_groupRepository, request.GroupId);
            //
            // // Vytvořím entitu naplněnou daty z příkazu.
            // var entity = CalendarEvent.Create(
            //     request,
            //     _currentUserService.User,
            //     channel,
            //     group,
            //     null,
            //     null
            // );
            //
            // // Přidám záznam do datového zdroje a uložím.
            // await _calendarEventRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            //
            // // Vrátím Id vytvořeného záznamu.
            // return _mapper.Map<CalendarEventDto>(entity);
            
            return _mapper.Map<CalendarEventDto>(null);
        }
    }
}