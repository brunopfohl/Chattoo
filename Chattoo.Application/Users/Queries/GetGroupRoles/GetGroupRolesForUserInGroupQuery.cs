using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Common.Services;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries
{
    /// <summary>
    /// Dotaz na skupiny, do kterých patří daný uživatel.
    /// </summary>
    public class GetGroupRolesForUserInGroupQuery : PaginatedQuery<GroupRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, pro kterého mají vyhledat uživatelské role.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů, ze které se hledají uživatelovy role.
        /// </summary>
        public string GroupId { get; set; }
    }

    public class GetGroupRolesForUserInGroupQueryHandler : PaginatedQueryHandler<GetGroupRolesForUserInGroupQuery, GroupRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public GetGroupRolesForUserInGroupQueryHandler(IMapper mapper, IGroupRepository groupRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public override async Task<PaginatedList<GroupRoleDto>> Handle(GetGroupRolesForUserInGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);
            var roles = group.Roles
                .Where(r => r.Users.Any(u => u.Id == request.UserId))
                .AsQueryable();
            
            // Načtu kolekci uživatelských rolí uživatele v dané skupině a zpracuju na stránkovanou kolekci.
            var result = await roles
                .ProjectTo<GroupRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}