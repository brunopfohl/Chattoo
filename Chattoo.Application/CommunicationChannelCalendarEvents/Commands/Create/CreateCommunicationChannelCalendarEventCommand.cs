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

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření kalendářní události v komunikačním kanálu.
    /// </summary>
    public class CreateCommunicationChannelCalendarEventCommand : IRequest<CommunicationChannelCalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje datum a čas počátku kalendářní události.
        /// </summary>
        public DateTime StartsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje datum a čas konce kalendářní události.
        /// </summary>
        public DateTime? EndsAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název události.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisek události.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string CommunicationChannelId { get; set; }
    }

    public class CreateCommunicationChannelCalendarEventCommandHandler : IRequestHandler<CreateCommunicationChannelCalendarEventCommand, CommunicationChannelCalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunicationChannelCalendarEventRepository _communicationChannelCalendarEventRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public CreateCommunicationChannelCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelCalendarEventRepository communicationChannelCalendarEventRepository, ICurrentUserService currentUserService, IMapper mapper, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelCalendarEventRepository = communicationChannelCalendarEventRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<CommunicationChannelCalendarEventDto> Handle(CreateCommunicationChannelCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // Získám komunikační kanál pomocí jeho Id.
            // Vyhodím výjimku, pokud komunikační kanál s předaným Id neexistuje.
            var channel = await _communicationChannelRepository.GetByIdAsync(request.CommunicationChannelId)
                         ?? throw new NotFoundException(nameof(CommunicationChannel), request.CommunicationChannelId);

            // Pokud uživatel není součástí komunikačního kanálu, vyhodím výjimku.
            if (!channel.Users.Contains(_currentUserService.User))
            {
                throw new ForbiddenAccessException();
            }
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannelCalendarEvent()
            {
                Name = request.Name,
                UserId = _currentUserService.User.Id,
                Description = request.Description,
                StartsAt = request.StartsAt,
                EndsAt = request.EndsAt,
                CommunicationChannelId = request.CommunicationChannelId,
                CommunicationChannel = channel
            };
            
            // Přidám záznam do datového zdroje a uložím.
            await _communicationChannelCalendarEventRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return _mapper.Map<CommunicationChannelCalendarEventDto>(entity);
        }
    }
}