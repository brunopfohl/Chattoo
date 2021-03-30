using System.Threading;
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

        public CreateCommunicationChannelMessageCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, ICommunicationChannelMessageRepository communicationChannelMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _communicationChannelMessageRepository = communicationChannelMessageRepository;
        }

        public async Task<string> Handle(CreateCommunicationChannelMessageCommand request, CancellationToken cancellationToken)
        {
            // Ověřím si, zda-li komunikační kanál skutečně existuje.
            _communicationChannelRepository.ThrowIfNotExists(request.ChannelId);
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannelMessage()
            {
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