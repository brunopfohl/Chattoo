using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands.User
{
    public class RemoveUserFromCalendarEventCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        
        public string EventId { get; set; }
    }
    
    public class RemoveUserFromCalendarEventCommandHandler : IRequestHandler<RemoveUserFromCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventManager _eventManager;

        public RemoveUserFromCalendarEventCommandHandler(IUnitOfWork unitOfWork, CalendarEventManager eventManager)
        {
            _unitOfWork = unitOfWork;
            _eventManager = eventManager;
        }

        public async Task<Unit> Handle(RemoveUserFromCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var calendarEvent = await _eventManager.GetEventOrThrow(request.EventId);

            _eventManager.RemoveParticipant(calendarEvent, request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}