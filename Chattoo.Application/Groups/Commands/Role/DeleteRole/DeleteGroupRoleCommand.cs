using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
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
        private readonly GroupManager _groupManager;

        public DeleteGroupRoleCommandHandler(IUnitOfWork unitOfWork, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _groupManager = groupManager;
        }

        public async Task<Unit> Handle(DeleteGroupRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            var role = _groupManager.GetRoleOrThrow(group, request.Id);

            group.DeleteRole(role);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}