using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání uživatele.
    /// </summary>
    public class DeleteUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má smazat.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi ho nepodaří dohledat).
            // var entity = await _userRepository.GetByIdAsync(request.Id, true);
            //
            // // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            // _userRepository.Remove(entity);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}