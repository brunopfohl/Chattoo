using System;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
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
        
        public int? MaximalParticipantsCount { get; set; }
    }
    
    public class UpdateCalendarEventCommandHandler : IRequestHandler<UpdateCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventManager _eventManager;

        public UpdateCalendarEventCommandHandler(IUnitOfWork unitOfWork, CalendarEventManager eventManager)
        {
            _unitOfWork = unitOfWork;
            _eventManager = eventManager;
        }

        public async Task<Unit> Handle(UpdateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var calendarEvent = await _eventManager.GetEventOrThrow(request.Id);

            await _eventManager.UpdateEvent
            (
                calendarEvent,
                request.Name,
                request.Description,
                request.MaximalParticipantsCount,
                request.StartsAt,
                request.EndsAt
            );
           
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}