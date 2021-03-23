using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.GroupRoles.Queries.GetForUserInGroup
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
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GetGroupRolesForUserInGroupQueryHandler(IMapper mapper, IUserRepository userRepository, IGroupRepository groupRepository, IGroupRoleRepository groupRoleRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
        }

        public override async Task<PaginatedList<GroupRoleDto>> Handle(GetGroupRolesForUserInGroupQuery request, CancellationToken cancellationToken)
        {
            // Nejdřív načtu uživatele s Id z požadavku, abych mohl určit, jestli uživatel skutečně existuje.
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }
            
            // Pokusím se načíst skupiny, pro kterou chci později vyhledat její uživatelské role.
            var group = await _groupRepository.GetByIdAsync(request.UserId);

            if (group is null)
            {
                throw new NotFoundException(nameof(Group), request.GroupId);
            }

            // Načtu kolekci uživatelských rolí uživatele v dané skupině a zpracuju na stránkovanou kolekci.
            var result = await _groupRoleRepository.GetForUserInGroup(request.UserId, request.GroupId)
                .ProjectTo<GroupRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}