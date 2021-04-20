using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření zprávy v komunikačním kanálu.
    /// </summary>
    public class CreateCommunicationChannelMessageCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele (autora) této zprávy.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato zpráva.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ zprávy.
        /// </summary>
        public CommunicationChannelMessageType Type { get; set; }
    }

    public class CreateCommunicationChannelMessageCommandHandler : IRequestHandler<CreateCommunicationChannelMessageCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICommunicationChannelMessageRepository _communicationChannelMessageRepository;
        private readonly IUserRepository _userRepository;

        public CreateCommunicationChannelMessageCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, ICommunicationChannelMessageRepository communicationChannelMessageRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _communicationChannelMessageRepository = communicationChannelMessageRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateCommunicationChannelMessageCommand request, CancellationToken cancellationToken)
        {
            // Pokusím se získat uživatele podle Id.
            // Ověřím si, zda-li uživatel existuje, pokud neexistuje, vyhodím výjimku.
            var user = await _userRepository.GetByIdAsync(request.UserId)
                          ?? throw new NotFoundException(nameof(User), request.UserId);
            
            // Ověřím si, zda-li komunikační kanál skutečně existuje.
            var channel = await _communicationChannelRepository.GetByIdAsync(request.ChannelId)
                          ?? throw new NotFoundException(nameof(Channel), request.ChannelId);

            // TODO: ověřit, že má na akci právo.
            
            // Ověřím si, zda-li je uživatel součástí komunikačního kanálu.
            if (!channel.Users.Contains(user))
            {
                throw new NotFoundException(
                    $"User with Id {request.UserId} is not a part of communication channel with Id {request.ChannelId}."
                );
            }
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannelMessage()
            {
                User = user,
                UserId = request.UserId,
                Channel = channel,
                ChannelId = request.ChannelId,
                Content = request.Content,
                Type = request.Type
            };

            // Přidám záznam do datového zdroje a uložím.
            await _communicationChannelMessageRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return entity.Id;
        }
    }
}