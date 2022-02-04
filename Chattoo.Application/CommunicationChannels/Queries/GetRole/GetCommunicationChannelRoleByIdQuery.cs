using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
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
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public GetCommunicationChannelRoleByIdQueryHandler(IMapper mapper, GetByIdUserSafeService getByIdUserSafeService, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _getByIdUserSafeService = getByIdUserSafeService;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<CommunicationChannelRoleDto> Handle(GetCommunicationChannelRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            var role = channel.Roles.FirstOrDefault(role => role.Id == request.RoleId);

            if (role == null)
            {
                throw new NotFoundException(nameof(CommunicationChannelRole), request.RoleId);
            }

            return _mapper.Map<CommunicationChannelRoleDto>(role);
        }
    }
}