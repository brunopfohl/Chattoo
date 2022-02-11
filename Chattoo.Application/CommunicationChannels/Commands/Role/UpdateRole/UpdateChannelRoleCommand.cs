using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující role z komunikačního kanálu.
    /// </summary>
    public class UpdateChannelRoleCommand : IRequest<CommunicationChannelRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }
    }
    
    public class UpdateChannelRoleCommandHandler : IRequestHandler<UpdateChannelRoleCommand, CommunicationChannelRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public UpdateChannelRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelRoleDto> Handle(UpdateChannelRoleCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var role = channel.GetRole(request.Id);
            
            channel.DeleteRole(role);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CommunicationChannelRoleDto>(role);
        }
    }
}