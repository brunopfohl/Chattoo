using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Commands
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
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserAliasCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteUserAliasCommand request, CancellationToken cancellationToken)
        {
            _currentUserService.User.DeleteAlias(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}