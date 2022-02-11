using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské role ve skupině uživatelů.
    /// </summary>
    public class AddGroupRoleCommand : IRequest<string>
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

    public class AddGroupRoleCommandHandler : IRequestHandler<AddGroupRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GroupManager _groupManager;

        public AddGroupRoleCommandHandler(IUnitOfWork unitOfWork, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _groupManager = groupManager;
        }

        public async Task<string> Handle(AddGroupRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            var role = group.AddRole(request.Name, request.Permission);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return role.Id;
        }
    }
}