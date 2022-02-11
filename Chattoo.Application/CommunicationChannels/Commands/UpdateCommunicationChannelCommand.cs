using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existujícího komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }
    }
    
    public class UpdateCommunicationChannelCommandHandler : IRequestHandler<UpdateCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public UpdateCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.Id);
            
            channel.SetName(request.Name);
            channel.SetDescription(request.Description);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}