using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
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
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public RemoveUserFromGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository,
            IUserRepository userRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<Unit> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);
            
            group.RemoveParticipant(request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}