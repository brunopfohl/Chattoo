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
        private readonly GroupManager _groupManager;

        public GetGroupRolesForUserInGroupQueryHandler(IMapper mapper, GroupManager groupManager)
        {
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public override async Task<PaginatedList<GroupRoleDto>> Handle(GetGroupRolesForUserInGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);
            
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