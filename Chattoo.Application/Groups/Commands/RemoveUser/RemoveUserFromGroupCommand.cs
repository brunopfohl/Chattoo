using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.RemoveUser
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

        public RemoveUserFromGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            // Vyhodím výjimku, pokud uživatel s předaným Id neexistuje.
            var user = await _userRepository.GetByIdAsync(request.UserId)
                       ?? throw new NotFoundException(nameof(User), request.UserId);

            // Vyhodím výjimku, pokud uživatel s předaným Id neexistuje.
            var group = await _groupRepository.GetByIdAsync(request.GroupId)
                        ?? throw new NotFoundException(nameof(Group), request.GroupId);

            // TODO: kontrola, že má uživatel právo na tuto akci.

            // Pokud uživatel není součástí skupiny, nelze ho z ní odebrat.
            if (!group.Users.Contains(user))
            {
                throw new NotFoundException(
                    $"User with Id {request.UserId} is not part of group with Id {request.GroupId}."
                );
            }

            // Odeberu uživatele ze skupiny.
            group.Users.Remove(user);

            // Promítnu změny do datového zdroje.
            _unitOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}