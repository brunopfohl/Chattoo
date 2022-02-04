using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
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
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public AddMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<string> Handle(AddMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            var attachment = channel.AddAttachment(request.MessageId, request.Name, request.Content, request.Type);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return attachment.Id;
        }
    }
}