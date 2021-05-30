using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání události z komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelCalendarEventCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id události z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelCalendarEventCommandHandler : IRequestHandler<DeleteCommunicationChannelCalendarEventCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelCalendarEventRepository _communicationChannelCalendarEventRepository;

        public DeleteCommunicationChannelCalendarEventCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelCalendarEventRepository communicationChannelCalendarEventRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelCalendarEventRepository = communicationChannelCalendarEventRepository;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelCalendarEventCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi to nepodaří).
            var entity = await _communicationChannelCalendarEventRepository.GetByIdAsync(request.Id, true);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _communicationChannelCalendarEventRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}