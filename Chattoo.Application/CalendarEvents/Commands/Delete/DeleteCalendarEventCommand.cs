using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalendarEventRepository _calendarEventRepository;

        public DeleteCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICalendarEventRepository calendarEventRepository)
        {
            _unitOfWork = unitOfWork;
            _calendarEventRepository = calendarEventRepository;
        }

        public async Task<Unit> Handle(DeleteCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // var entity = await _getByIdUserSafeService.GetAsync(_calendarEventRepository, request.Id);
            //
            // // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            // _calendarEventRepository.Remove(entity);
            //
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}