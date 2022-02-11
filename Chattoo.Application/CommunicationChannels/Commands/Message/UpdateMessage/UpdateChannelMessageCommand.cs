using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující zprávy z komunikačního kanálu.
    /// </summary>
    public class UpdateChannelMessageCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
    }
    
    public class UpdateChannelMessageCommandHandler : IRequestHandler<UpdateChannelMessageCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public UpdateChannelMessageCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
        }

        public async Task<Unit> Handle(UpdateChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            _channelManager.UpdateMessage(channel, request.Id, request.Content);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}