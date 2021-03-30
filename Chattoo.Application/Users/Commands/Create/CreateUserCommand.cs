using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské přezdívky.
    /// </summary>
    public class CreateUserCommand : IRequest<string>
    {
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Pokusím se z datového zdroje vytáhnout uživatele, kterému se má přidat přezdívka.
            var user = _currentUserService.User;

            // Nového uživatele aplikační vrstvy má smysl tvořit, pouze pokud ještě neexistuje.
            if (user is null)
            {
                // Vytvořím entitu naplněnou daty z příkazu.
                var entity = new User()
                {
                    Id = _currentUserService.UserId
                };
                
                // Přidám záznam do datového zdroje a uložím.`
                await _userRepository.AddOrUpdateAsync(entity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            // Vrátím Id vytvořeného záznamu.
            return _currentUserService.UserId;
        }
    }
}