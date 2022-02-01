using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující role z komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelRoleCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id role z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }
    }
    
    public class UpdateCommunicationChannelRoleCommandHandler : IRequestHandler<UpdateCommunicationChannelRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommunicationChannelRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelRoleCommand request, CancellationToken cancellationToken)
        {
            // // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi to nepodaří).
            // var entity = await _communicationChannelRoleRepository.GetByIdAsync(request.Id, true);
            //
            // // Naplním entitu daty z příkazu.
            // entity.Name = request.Name;
            // entity.Permission = request.Permission;
            //
            // // Upravím záznam a uložím.
            // await _communicationChannelRoleRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}