using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Domain.Services;

namespace Chattoo.Application.CommunicationChannels.Queries.GetRoles
{
    public class GetCommunicationChannelRolesQuery : PaginatedQuery<CommunicationChannelRoleDto>
    {
        public string ChannelId { get; set; }
    }

    public class GetCommunicationChannelRolesQueryHandler : PaginatedQueryHandler<GetCommunicationChannelRolesQuery, CommunicationChannelRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;
        
        public GetCommunicationChannelRolesQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public override async Task<PaginatedList<CommunicationChannelRoleDto>> Handle(GetCommunicationChannelRolesQuery request,
            CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var roles = channel.Roles.AsQueryable();
            
            var result = await roles
                .ProjectTo<CommunicationChannelRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}