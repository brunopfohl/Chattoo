using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Commands.Create
{
    public class CreateCalendarEventWishCommand : IRequest<CalendarEventWishDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny lidí, se kterými má uživatel zájem zorganizovat událost.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačího kanálu, jehož členové se do události mohou zapojit.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int? MinimalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje maximální počet účastníků.
        /// </summary>
        public int? MaximalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci typů událostí, o které má uživatel zájem.
        /// </summary>
        public virtual ICollection<CalendarEventTypeDto> Types { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje vytvoření události.
        /// </summary>
        public ICollection<DateIntervalDto> DateIntervals { get; set; }
    }

    public class CreateCalendarEventWishCommandHandler : IRequestHandler<CreateCalendarEventWishCommand, CalendarEventWishDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalendarEventWishRepository _calendarEventWishRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork,
            ICalendarEventWishRepository calendarEventWishRepository, ICurrentUserService currentUserService,
            IMapper mapper, ICommunicationChannelRepository communicationChannelRepository,
            IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _calendarEventWishRepository = calendarEventWishRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _groupRepository = groupRepository;
        }
        
        public async Task<CalendarEventWishDto> Handle(CreateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            // CommunicationChannel channel =
            //     await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.CommunicationChannelId);
            //
            // Group group =
            //     await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);
            //
            // var entity = CalendarEventWish.Create(
            //     _currentUserService.User,
            //     channel,
            //     group,
            //     request.MinimalParticipantsCount,
            //     request.MaximalParticipantsCount,
            //     request.DateIntervals as ICollection<IDateInterval>,
            //     request.Types as ICollection<ICalendarEventType>);
            //
            // // Přidám záznam do datového zdroje a uložím.
            // await _calendarEventWishRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            //
            // // Vrátím Id vytvořeného záznamu.
            // return _mapper.Map<CalendarEventWishDto>(entity);
            return _mapper.Map<CalendarEventWishDto>(null);
        }
    }
}