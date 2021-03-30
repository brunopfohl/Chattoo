using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessageAttachments.Commands.Create
{
    /// <summary>
    /// Příkaz pro přidání přílohy ke zprávě z komunikačního kanálu.
    /// </summary>
    public class CreateCommunicationChannelMessageAttachmentCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy, pod kterou spadá tato příloha.
        /// </summary>
        public string MessageId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název přílohy.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje data přílohy.
        /// </summary>
        public byte[] Content { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ přílohy.
        /// </summary>
        public CommunicationChannelMessageAttachmentType Type { get; set; }
    }

    public class CreateCommunicationChannelMessageAttachmentCommandHandler : IRequestHandler<CreateCommunicationChannelMessageAttachmentCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelMessageAttachmentRepository _communicationChannelMessageAttachmentRepository;
        private readonly ICommunicationChannelMessageRepository _communicationChannelMessageRepository;

        public CreateCommunicationChannelMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelMessageAttachmentRepository communicationChannelMessageAttachmentRepository, ICommunicationChannelMessageRepository communicationChannelMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelMessageAttachmentRepository = communicationChannelMessageAttachmentRepository;
            _communicationChannelMessageRepository = communicationChannelMessageRepository;
        }

        public async Task<string> Handle(CreateCommunicationChannelMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            // Pokusím se z datového zdroje získat zprávu z komunikačního kanálu.
            _communicationChannelMessageRepository.ThrowIfNotExists(request.MessageId);
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannelMessageAttachment()
            {
                MessageId = request.MessageId,
                Name = request.Name,
                Content = request.Content,
                Type = request.Type
            };

            // Přidám záznam do datového zdroje a uložím.
            await _communicationChannelMessageAttachmentRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return entity.Id;
        }
    }
}