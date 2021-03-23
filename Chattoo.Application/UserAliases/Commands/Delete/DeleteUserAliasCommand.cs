using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.UserAliases.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání uživatelovi přezdívky.
    /// </summary>
    public class DeleteUserAliasCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatelské přezdívky, která se má smazat.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteUserAliasCommandHandler : IRequestHandler<DeleteUserAliasCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAliasRepository _userAliasRepository;

        public DeleteUserAliasCommandHandler(IUnitOfWork unitOfWork, IUserAliasRepository userAliasRepository)
        {
            _unitOfWork = unitOfWork;
            _userAliasRepository = userAliasRepository;
        }

        public async Task<Unit> Handle(DeleteUserAliasCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje.
            var entity = await _userAliasRepository.GetByIdAsync(request.Id);

            // Pokud se mi záznam nepodařilo najít, vrátím NotFoundException (zdroj nenalezen).
            if (entity is null)
            {
                throw new NotFoundException(nameof(UserAlias), request.Id);
            }
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _userAliasRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}