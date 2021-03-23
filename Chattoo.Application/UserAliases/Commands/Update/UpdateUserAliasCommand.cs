using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.UserAliases.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující přezdívky uživatele.
    /// </summary>
    public class UpdateUserAliasCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatelské přezdívky.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje přezdívku uživatele.
        /// </summary>
        public string Alias { get; set; }
    }
    
    public class UpdateUserAliasCommandHandler : IRequestHandler<UpdateUserAliasCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAliasRepository _userAliasRepository;

        public UpdateUserAliasCommandHandler(IUnitOfWork unitOfWork, IUserAliasRepository userAliasRepository)
        {
            _unitOfWork = unitOfWork;
            _userAliasRepository = userAliasRepository;
        }

        public async Task<Unit> Handle(UpdateUserAliasCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje.
            var entity = await _userAliasRepository.GetByIdAsync(request.Id);

            // Pokud se mi záznam nepodařilo najít, vrátím NotFoundException (zdroj nenalezen).
            if (entity is null)
            {
                throw new NotFoundException(nameof(UserAlias), request.Id);
            }

            // Naplním entitu daty z příkazu.
            entity.Alias = request.Alias;

            // Přidám záznam do datového zdroje a uložím.`
            await _userAliasRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}