using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro přidání přílohy ke zprávě z komunikačního kanálu.
    /// </summary>
    public class AddMessageAttachmentCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
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

    public class AddMessageAttachmentCommandHandler : IRequestHandler<AddMessageAttachmentCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;
        private readonly MessageManager _messageManager;

        public AddMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager, MessageManager messageManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
            _messageManager = messageManager;
        }

        public async Task<string> Handle(AddMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            var message = _channelManager.GetMessageOrThrow(channel, request.MessageId);

            var attachment =
                _messageManager.AddAttachment(message, request.Name, request.Content, request.Type);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return attachment.Id;
        }
    }
}