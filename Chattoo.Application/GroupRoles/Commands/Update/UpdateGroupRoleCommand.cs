using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.GroupRoles.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující uživatelské role v rámci skupiny.
    /// </summary>
    public class UpdateGroupRoleCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatelské role v rámci skupiny.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role v rámci skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje oprávnění, které tato uživatelská role poskytuje.
        /// </summary>
        public UserGroupPermission Permission { get; set; }
    }
    
    public class UpdateGroupRoleCommandHandler : IRequestHandler<UpdateGroupRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public UpdateGroupRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Unit> Handle(UpdateGroupRoleCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi ho nepodaří dohledat).
            var entity = await _groupRoleRepository.GetByIdAsync(request.Id, true);

            // Naplním entitu daty z příkazu.
            entity.Name = request.Name;
            entity.Permission = request.Permission;

            // Přidám záznam do datového zdroje a uložím.
            await _groupRoleRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}