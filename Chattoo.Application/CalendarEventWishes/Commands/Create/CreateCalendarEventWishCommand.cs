using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
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
using Chattoo.Domain.Services;
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
        private readonly IMapper _mapper;
        private readonly GroupManager _groupManager;
        private readonly ChannelManager _channelManager;

        public CreateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupManager groupManager, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _groupManager = groupManager;
            _channelManager = channelManager;
        }

        public async Task<CalendarEventWishDto> Handle(CreateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);
            var channel = await _groupManager.GetGroupOrThrow(request.CommunicationChannelId);
            
            var wish = CalendarEventWish.Create()
            
            return _mapper.Map<CalendarEventWishDto>(null);
        }
    }
}