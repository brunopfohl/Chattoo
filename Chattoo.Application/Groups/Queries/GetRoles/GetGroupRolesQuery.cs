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

namespace Chattoo.Application.Groups.Queries.GetRoles
{
    public class GetGroupRolesQuery : PaginatedQuery<GroupRoleDto>
    {
        public string GroupId { get; set; }
    }
    
    public class GetGroupRolesQueryHandler : PaginatedQueryHandler<GetGroupRolesQuery, GroupRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly GroupManager _groupManager;
        
        public GetGroupRolesQueryHandler(IMapper mapper, GroupManager groupManager)
        {
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public override async Task<PaginatedList<GroupRoleDto>> Handle(GetGroupRolesQuery request,
            CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            var roles = group.Roles.AsQueryable();
            
            var result = await roles
                .ProjectTo<GroupRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}