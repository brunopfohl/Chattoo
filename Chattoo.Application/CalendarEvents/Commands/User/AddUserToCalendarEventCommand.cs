using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands.User
{
    public class AddUserToCalendarEventCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        
        public string EventId { get; set; }
    }
    
    public class AddUserToCalendarEventCommandHandler : IRequestHandler<AddUserToCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventManager _eventManager;
        private readonly UserManager _userManager;

        public AddUserToCalendarEventCommandHandler(IUnitOfWork unitOfWork, CalendarEventManager eventManager, UserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _eventManager = eventManager;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(AddUserToCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var calendarEvent = await _eventManager.GetEventOrThrow(request.EventId);
            var user = await _userManager.GetUserOrThrow(request.UserId);

            _eventManager.AddParticipant(calendarEvent, user);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}