using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
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
        private readonly GroupManager _groupManager;

        public AddUserToGroupCommandHandler(IUnitOfWork unitOfWork, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _groupManager = groupManager;
        }

        public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            await _groupManager.AddParticipantToGroup(group, request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}