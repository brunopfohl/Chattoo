using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEvents.Commands
{
    public class UpdateCalendarEventWishCommand : IRequest
    {
        public string Id { get; set; }
        
        public string CommunicationChannelId { get; set; }
        
        public int? MinimalParticipantsCount { get; set; }
        
        public int? MaximalParticipantsCount { get; set; }
        
        public virtual ICollection<string> TypeIds { get; set; }
        
        public ICollection<DateIntervalDto> DateIntervals { get; set; }
    }
    
    public class UpdateCalendarEventWishCommandHandler : IRequestHandler<UpdateCalendarEventWishCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CalendarEventWishManager _wishManager;

        public UpdateCalendarEventWishCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCalendarEventWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishManager.GetWishOrThrow(request.Id);

            await _wishManager.UpdateWish
            (
                wish,
                request.TypeIds,
                request.DateIntervals as ICollection<IDateInterval>,
                request.MinimalParticipantsCount,
                request.MaximalParticipantsCount
            );
           
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}