using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    /// <summary>
    /// Dotaz na uživatelskou roli (z komunikačního kanálu) s daným Id.
    /// </summary>
    public class GetCommunicationChannelRoleByIdQuery : IRequest<CommunicationChannelRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role z komunikačního kanálu.
        /// </summary>
        public string RoleId { get; set; }
    }
    
    public class GetCommunicationChannelRoleByIdQueryHandler : IRequestHandler<GetCommunicationChannelRoleByIdQuery, CommunicationChannelRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;

        public GetCommunicationChannelRoleByIdQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelRoleDto> Handle(GetCommunicationChannelRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            var role = _channelManager.GetRoleOrThrow(channel, request.RoleId);

            return _mapper.Map<CommunicationChannelRoleDto>(role);
        }
    }
}