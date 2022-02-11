using System.Threading;
using System.Threading.Tasks;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské role v komunikačním kanálu.
    /// </summary>
    public class AddChannelRoleCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato role.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název role.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }
    }

    public class AddChannelRoleCommandHandler : IRequestHandler<AddChannelRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public AddChannelRoleCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
        }

        public async Task<string> Handle(AddChannelRoleCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var role = channel.AddRole(request.Name, request.Permission);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return role.Id;
        }
    }
}