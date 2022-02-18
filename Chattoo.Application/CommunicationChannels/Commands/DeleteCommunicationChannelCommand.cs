using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro smazání komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelCommandHandler : IRequestHandler<DeleteCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;
        private readonly ICommunicationChannelRepository _channelRepository;

        public DeleteCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager, ICommunicationChannelRepository channelRepository)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
            _channelRepository = channelRepository;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            // TODO: Kanál by asi měl mít jasného majitele (nebo roli).
            
            var channel = await _channelManager.GetChannelOrThrow(request.Id);
            
            _channelRepository.Remove(channel);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}