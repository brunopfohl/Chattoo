using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.RemoveUser
{
    /// <summary>
    /// Příkaz pro odebrání uživatele z komunikačního kanálu.
    /// </summary>
    public class RemoveUserFromCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má odebrat ze skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, ze kterého se má odebrat uživatel.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class RemoveUserFromCommunicationChannelCommandHandler : IRequestHandler<RemoveUserFromCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly IUserRepository _userRepository;

        public RemoveUserFromCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RemoveUserFromCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            // Získám uživatele pomocí jeho Id.
            // Vyhodím výjimku, pokud uživatel s předaným Id neexistuje.
            var user = await _userRepository.GetByIdAsync(request.UserId)
                         ?? throw new NotFoundException(nameof(User), request.UserId);
            // Získám komunikační kanál pomocí jeho Id.
            // Vyhodím výjimku, pokud uživatel s předaným Id neexistuje.
            var channel = await _communicationChannelRepository.GetByIdAsync(request.ChannelId)
                         ?? throw new NotFoundException(nameof(CommunicationChannel), request.ChannelId);
            
            // TODO: kontrola, že má uživatel právo na tuto akci.
            
            // Pokud uživatel není součástí komunikačního kanálu, nelze ho z něj odebrat.
            if (!channel.Users.Contains(user))
            {
                throw new NotFoundException(
                    $"User with Id {request.UserId} is not part of communication channel with Id {request.ChannelId}."
                );
            }
            
            // Promítnu změny do datového zdroje.
            _unitOfWork.SaveChanges();
            
            return Unit.Value;
        }
    }
}