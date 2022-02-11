using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteMessageAttachmentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy.
        /// </summary>
        public string MessageId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy v komunikačním kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteMessageAttachmentCommandHandler : IRequestHandler<DeleteMessageAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;
        private readonly MessageManager _messageManager;

        public DeleteMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, ChannelManager channelManager, MessageManager messageManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
            _messageManager = messageManager;
        }

        public async Task<Unit> Handle(DeleteMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            var message = _channelManager.GetMessageOrThrow(channel, request.MessageId);

            _messageManager.DeleteAttachment(message, request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}