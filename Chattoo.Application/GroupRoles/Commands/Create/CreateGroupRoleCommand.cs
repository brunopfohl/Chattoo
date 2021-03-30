using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.GroupRoles.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské role ve skupině uživatelů.
    /// </summary>
    public class CreateGroupRoleCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, do které se má přidat nová role.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role v rámci skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje oprávnění, které tato role umožňuje.
        /// </summary>
        public UserGroupPermission Permission { get; set; }
    }

    public class CreateGroupRoleCommandHandler : IRequestHandler<CreateGroupRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public CreateGroupRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository, IGroupRepository groupRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
            _groupRepository = groupRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateGroupRoleCommand request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst skupinu uživatelů, do které se má přidat nová role (vyhodím výjimku, pokud ji nedohledám).
            var group = await _groupRepository.GetByIdAsync(request.GroupId, true);

            // Připravím si role aktuálně přihlášeného uživatele, abych věděl jestli má právo na přidání role.
            var currentUserRoles = _groupRoleRepository.GetForUserInGroup(_currentUserService.UserId, request.GroupId);
            
            // Pokud nemá aktuálně přihlášený uživatel právo na přidání role, vyhodím výjimku.
            if (group.CreatedBy != _currentUserService.UserId && currentUserRoles.Any(r => r.Permission == UserGroupPermission.Admin))
            {
                throw new ForbiddenAccessException();
            }
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new GroupRole()
            {
                GroupId = request.GroupId,
                Name = request.Name,
                Permission = request.Permission
            };

            // Přidám záznam do datového zdroje a uložím.`
            await _groupRoleRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return entity.Id;
        }
    }
}