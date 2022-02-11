using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.Groups.Queries
{
    /// <summary>
    /// Dotaz na uživatelskou roli s daným Id.
    /// </summary>
    public class GetGroupRoleByIdQuery : IRequest<GroupRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string GroupId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role.
        /// </summary>
        public string RoleId { get; set; }
    }
    
    public class GetGroupRoleByIdQueryHandler : IRequestHandler<GetGroupRoleByIdQuery, GroupRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly GroupManager _groupManager;

        public GetGroupRoleByIdQueryHandler(IMapper mapper, GroupManager groupManager)
        {
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public async Task<GroupRoleDto> Handle(GetGroupRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            var role = _groupManager.GetRoleOrThrow(group, request.RoleId);

            return _mapper.Map<GroupRoleDto>(role);
        }
    }
}