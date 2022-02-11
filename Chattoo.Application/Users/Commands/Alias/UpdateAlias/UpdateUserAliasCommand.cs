using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Commands
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
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserAliasCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateUserAliasCommand request, CancellationToken cancellationToken)
        {
            _currentUserService.User.UpdateAlias(request.Id, request.Alias);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}