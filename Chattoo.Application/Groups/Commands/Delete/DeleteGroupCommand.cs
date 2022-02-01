using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání skupiny uživatelů.
    /// </summary>
    public class DeleteGroupCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, která se má smazat.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se mi ho nepodaří dohledat).
            var entity = await _groupRepository.GetByIdAsync(request.Id) 
                         ?? throw new NotFoundException(nameof(Group), request.Id);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _groupRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}