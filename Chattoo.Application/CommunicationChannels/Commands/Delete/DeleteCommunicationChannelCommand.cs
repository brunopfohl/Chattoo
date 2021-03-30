using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelCommandHandler : IRequestHandler<DeleteCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public DeleteCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi to nepodaří).
            var entity = await _communicationChannelRepository.GetByIdAsync(request.Id, true);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _communicationChannelRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}