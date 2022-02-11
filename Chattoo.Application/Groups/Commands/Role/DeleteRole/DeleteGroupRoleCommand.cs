using System.Threading;
using System.Threading.Tasks;
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
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);

            group.DeleteRole(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}