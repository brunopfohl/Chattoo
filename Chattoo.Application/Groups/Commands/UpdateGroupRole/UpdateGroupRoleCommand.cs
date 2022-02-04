using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující uživatelské role v rámci skupiny.
    /// </summary>
    public class UpdateGroupRoleCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny.
        /// </summary>
        public string GroupId { get; set; }
        
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
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupRoleCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(UpdateGroupRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);

            group.UpdateRole(request.Id, request.Name, request.Permission);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}