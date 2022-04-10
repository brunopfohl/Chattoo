using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    public class CreateCalendarEventWishCommand : IRequest<CalendarEventWishDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačího kanálu, jehož členové se do události mohou zapojit.
        /// </summary>
        public string CommunicationChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje minimální počet účastníků.
        /// </summary>
        public int? MinimalParticipantsCount { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ události.
        /// </summary>
        public CalendarEventType Type { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci časových bloků, kdy si uživatel přeje vytvoření události.
        /// </summary>
        public ICollection<DateIntervalDto> DateIntervals { get; set; }
    }

    public class CreateCalendarEventWishCommandHandler : IRequestHandler<CreateCalendarEventWishCommand, CalendarEventWishDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventWishManager _wishManager;
        private readonly ICalendarEventWishRepository _wishRepository;
        private readonly IMapper _mapper;

        public CreateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupManager groupManager, ChannelManager channelManager, CalendarEventWishManager wishManager, ICalendarEventWishRepository wishRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wishManager = wishManager;
            _wishRepository = wishRepository;
        }

        public async Task<CalendarEventWishDto> Handle(CreateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishManager.Create
            (
                request.CommunicationChannelId,
                request.DateIntervals as ICollection<IDateInterval>,
                request.Type,
                request.MinimalParticipantsCount
            );

            await _wishRepository.AddOrUpdateAsync(wish, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CalendarEventWishDto>(null);
        }
    }
}