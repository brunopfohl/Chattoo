using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské přezdívky.
    /// </summary>
    public class AddUserAliasCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, kterému se má vytvořit nová přezdívka.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje přezdívku uživatele.
        /// </summary>
        public string Alias { get; set; }
    }

    public class AddUserAliasCommandHandler : IRequestHandler<AddUserAliasCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public AddUserAliasCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(AddUserAliasCommand request, CancellationToken cancellationToken)
        {
            var alias = _currentUserService.User.AddAlias(request.Alias);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            // Vrátím Id vytvořeného záznamu.
            return alias.Id;
        }
    }
}