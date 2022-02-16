using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    public class DeleteCalendarEventWishCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class DeleteCommandCalendarEventWishCommandHandler : IRequestHandler<DeleteCalendarEventWishCommand, Unit>
    {
        private readonly CalendarEventWishManager _wishManager;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandCalendarEventWishCommandHandler(CalendarEventWishManager wishManager, IUnitOfWork unitOfWork)
        {
            _wishManager = wishManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            await _wishManager.DeleteWish(request.Id);
            
            await  _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}