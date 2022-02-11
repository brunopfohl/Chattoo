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
    public class DeleteChannelRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role y komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteChannelRoleCommandHandler : IRequestHandler<DeleteChannelRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public DeleteChannelRoleCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
        }

        public async Task<Unit> Handle(DeleteChannelRoleCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var role = _channelManager.GetRoleOrThrow(channel, request.Id);

            channel.DeleteRole(role);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}