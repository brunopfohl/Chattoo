using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    public class RemoveUserFromGroupCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má odebrat ze skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, ze které se má odebrat uživatel.
        /// </summary>
        public string GroupId { get; set; }
    }

    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GroupManager _groupManager;

        public RemoveUserFromGroupCommandHandler(IUnitOfWork unitOfWork, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _groupManager = groupManager;
        }

        public async Task<Unit> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);
            
            group.RemoveParticipant(request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}