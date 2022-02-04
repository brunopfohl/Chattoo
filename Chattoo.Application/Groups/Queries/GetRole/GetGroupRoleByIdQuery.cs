using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
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
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly IGroupRepository _groupRepository;

        public GetGroupRoleByIdQueryHandler(IMapper mapper, GetByIdUserSafeService getByIdUserSafeService, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _getByIdUserSafeService = getByIdUserSafeService;
            _groupRepository = groupRepository;
        }

        public async Task<GroupRoleDto> Handle(GetGroupRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);
            var role = group.Roles.FirstOrDefault(r => r.Id == request.RoleId);

            if (role == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<GroupRoleDto>(role);
        }
    }
}