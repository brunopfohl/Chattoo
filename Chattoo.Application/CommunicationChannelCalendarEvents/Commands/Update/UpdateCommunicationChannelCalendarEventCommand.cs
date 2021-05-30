using System;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující kalendářní události z komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelCalendarEventCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id kalendářní události z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
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
    }
    
    public class UpdateCommunicationChannelCalendarEventCommandHandler : IRequestHandler<UpdateCommunicationChannelCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunicationChannelCalendarEventRepository _communicationChannelCalendarEventRepository;

        public UpdateCommunicationChannelCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelCalendarEventRepository communicationChannelCalendarEventRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelCalendarEventRepository = communicationChannelCalendarEventRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje.
            var entity = await _communicationChannelCalendarEventRepository.GetByIdAsync(request.Id)
                         ?? throw new NotFoundException(nameof(CommunicationChannelCalendarEvent), request.Id);

            // Pokud uživatel nemá dostatečná práva vyhodím výjimku.
            if (entity.UserId != _currentUserService.User.Id)
            {
                throw new ForbiddenAccessException();
            }

            // Naplním entitu daty z příkazu.
            entity.StartsAt = request.StartsAt;
            entity.EndsAt = request.EndsAt;
            entity.Name = request.Name;
            entity.Description = request.Description;

            // Přidám záznam do datového zdroje a uložím.`
            await _communicationChannelCalendarEventRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}