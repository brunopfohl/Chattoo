using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující skupiny.
    /// </summary>
    public class UpdateGroupCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }
    
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje.
            var entity = await _groupRepository.GetByIdAsync(request.Id);

            // Pokud se mi záznam nepodařilo najít, vrátím NotFoundException (zdroj nenalezen).
            if (entity is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            // Naplním entitu daty z příkazu.
            entity.Name = request.Name;

            // Přidám záznam do datového zdroje a uložím.`
            await _groupRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}