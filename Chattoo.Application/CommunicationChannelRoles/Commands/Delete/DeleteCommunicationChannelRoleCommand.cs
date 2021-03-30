using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id role y komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelRoleCommandHandler : IRequestHandler<DeleteCommunicationChannelRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRoleRepository _communicationChannelRoleRepository;

        public DeleteCommunicationChannelRoleCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRoleRepository communicationChannelRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRoleRepository = communicationChannelRoleRepository;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelRoleCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi to nepodaří).
            var entity = await _communicationChannelRoleRepository.GetByIdAsync(request.Id, true);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _communicationChannelRoleRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}