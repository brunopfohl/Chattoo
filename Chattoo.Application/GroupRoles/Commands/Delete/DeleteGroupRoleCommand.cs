using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.GroupRoles.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání uživatelské role.
    /// </summary>
    public class DeleteGroupRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatelské role.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteGroupRoleCommandHandler : IRequestHandler<DeleteGroupRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public DeleteGroupRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Unit> Handle(DeleteGroupRoleCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi ho nepodaří dohledat).
            var entity = await _groupRoleRepository.GetByIdAsync(request.Id, true);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _groupRoleRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}