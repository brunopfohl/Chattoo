using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro přidání uživatele do skupiny uživatelů.
    /// </summary>
    public class AddUserToGroupCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má přidat do skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, do které se má přidat uživatel.
        /// </summary>
        public string GroupId { get; set; }
    }

    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public AddUserToGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository,
            GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);
            
            group.AddParticipant(request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}