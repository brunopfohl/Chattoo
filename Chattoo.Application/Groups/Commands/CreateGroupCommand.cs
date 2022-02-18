using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření skupiny uživatelů.
    /// </summary>
    public class CreateGroupCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }

    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GroupManager _groupManager;
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, GroupManager groupManager, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupManager = groupManager;
            _groupRepository = groupRepository;
        }

        public async Task<string> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _groupManager.Create(request.Name);
            
            await _groupRepository.AddOrUpdateAsync(group, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return group.Id;
        }
    }
}