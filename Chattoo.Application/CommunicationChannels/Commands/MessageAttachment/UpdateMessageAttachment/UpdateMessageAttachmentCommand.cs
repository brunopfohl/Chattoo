using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující přílohy zprávy z komunikačního kanálu.
    /// </summary>
    public class UpdateMessageAttachmentCommand : IRequest
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
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název přílohy.
        /// </summary>
        public string Name { get; set; }
    }
    
    public class UpdateMessageAttachmentCommandHandler : IRequestHandler<UpdateMessageAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;
        private readonly MessageManager _messageManager;

        public UpdateMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager, MessageManager messageManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
            _messageManager = messageManager;
        }

        public async Task<Unit> Handle(UpdateMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            var message = _channelManager.GetMessageOrThrow(channel, request.MessageId);

            _messageManager.UpdateAttachment(message, request.Id, request.Name);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}