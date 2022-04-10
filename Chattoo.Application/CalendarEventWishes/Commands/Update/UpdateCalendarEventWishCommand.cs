using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    public class UpdateCalendarEventWishCommand : IRequest
    {
        public string Id { get; set; }
        
        public int? MinimalParticipantsCount { get; set; }
        
        public CalendarEventType Type { get; set; }
        
        public ICollection<DateIntervalDto> DateIntervals { get; set; }
    }
    
    public class UpdateCalendarEventWishCommandHandler : IRequestHandler<UpdateCalendarEventWishCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventWishManager _wishManager;

        public UpdateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork, CalendarEventWishManager wishManager)
        {
            _unitOfWork = unitOfWork;
            _wishManager = wishManager;
        }

        public async Task<Unit> Handle(UpdateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishManager.GetWishOrThrow(request.Id);

            await _wishManager.UpdateWish
            (
                wish,
                request.Type,
                request.DateIntervals as ICollection<IDateInterval>,
                request.MinimalParticipantsCount
            );
           
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}