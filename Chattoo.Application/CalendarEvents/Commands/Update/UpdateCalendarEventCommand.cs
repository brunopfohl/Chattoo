using System;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující kalendářní události z komunikačního kanálu.
    /// </summary>
    public class UpdateCalendarEventCommand : IRequest
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
    
    public class UpdateCalendarEventCommandHandler : IRequestHandler<UpdateCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICalendarEventRepository _calendarEventRepository;

        public UpdateCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICalendarEventRepository calendarEventRepository,
            ICurrentUserService currentUserService, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _calendarEventRepository = calendarEventRepository;
            _currentUserService = currentUserService;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<Unit> Handle(UpdateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // var entity = await _getByIdUserSafeService.GetAsync(_calendarEventRepository, request.Id);
            //
            // // Pokud uživatel nemá dostatečná práva vyhodím výjimku.
            // if (entity.AuthorId != _currentUserService.User.Id)
            // {
            //     throw new ForbiddenAccessException();
            // }
            //
            // // Naplním entitu daty z příkazu.
            // entity.StartsAt = request.StartsAt;
            // entity.EndsAt = request.EndsAt;
            // entity.Name = request.Name;
            // entity.Description = request.Description;
            //
            // // Přidám záznam do datového zdroje a uložím.`
            // await _calendarEventRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}