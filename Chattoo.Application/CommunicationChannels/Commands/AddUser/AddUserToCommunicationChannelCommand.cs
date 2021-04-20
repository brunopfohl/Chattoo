using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.AddUser
{
    /// <summary>
    /// Příkaz pro přidání uživatele do komunikačního kanálu.
    /// </summary>
    public class AddUserToCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má přidat do skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, do kterého se má přidat uživatel.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class AddUserToCommunicationChannelCommandHandler : IRequestHandler<AddUserToCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly IUserRepository _userRepository;

        public AddUserToCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddUserToCommunicationChannelCommand request, CancellationToken cancellationToken)
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
            
            // Přidám uživatele do skupiny.
            channel.Users.Add(user);
            
            // Promítnu změny do datového zdroje.
            _unitOfWork.SaveChanges();
            
            return Unit.Value;
        }
    }
}