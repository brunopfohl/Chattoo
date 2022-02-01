using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.AddUser
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
        private readonly IUserRepository _userRepository;

        public AddUserToGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            // // Vyhodím výjimku, pokud uživatel s předaným Id neexistuje.
            // var user = await _userRepository.GetByIdAsync(request.UserId)
            //              ?? throw new NotFoundException(nameof(User), request.UserId);
            // // Vyhodím výjimku, pokud skupina s předaným Id neexistuje.
            // var group = await _groupRepository.GetByIdAsync(request.GroupId)
            //              ?? throw new NotFoundException(nameof(Group), request.GroupId);
            //
            // // TODO: kontrola, že má uživatel právo na tuto akci.
            //
            // // Přidám uživatele do skupiny.
            // group.Users.Add(user);
            //
            // // Promítnu změny do datového zdroje.
            // _unitOfWork.SaveChanges();
            
            return Unit.Value;
        }
    }
}