using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Příkaz pro smazání události z komunikačního kanálu.
    /// </summary>
    public class DeleteCalendarEventCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id události z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCalendarEventCommandHandler : IRequestHandler<DeleteCalendarEventCommand, Unit>
    {
        private readonly CalendarEventManager _eventManager;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteCalendarEventCommandHandler(CalendarEventManager eventManager, IUnitOfWork unitOfWork)
        {
            _eventManager = eventManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCalendarEventCommand request, CancellationToken cancellationToken)
        {
            await _eventManager.DeleteEvent(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}