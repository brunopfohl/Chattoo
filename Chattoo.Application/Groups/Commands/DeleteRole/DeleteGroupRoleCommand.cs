using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro smazání uživatelské role.
    /// </summary>
    public class DeleteGroupRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id uživatelské role.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteGroupRoleCommandHandler : IRequestHandler<DeleteGroupRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupRoleCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);

            group.DeleteRole(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}