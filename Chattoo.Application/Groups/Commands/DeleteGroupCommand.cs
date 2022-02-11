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
        private readonly GroupManager _groupManager;

        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupManager = groupManager;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            // TODO: Buď by mazání skupiny měl mít povolený nějaký owner nebo alespoň uživatel, co je součástí nějaké skupiny.
            
            var group = await _groupManager.GetGroupOrThrow(request.Id);
            
            _groupRepository.Remove(group);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}