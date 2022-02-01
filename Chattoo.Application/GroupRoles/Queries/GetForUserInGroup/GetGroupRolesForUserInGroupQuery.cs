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

        public GetGroupRolesForUserInGroupQueryHandler(IMapper mapper, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public override async Task<PaginatedList<GroupRoleDto>> Handle(GetGroupRolesForUserInGroupQuery request, CancellationToken cancellationToken)
        {
            // // Zkontoluji, zda-li uživatel existuje.
            // _userRepository.ThrowIfNotExists(request.UserId);
            // // Zkontroluji, zda-li skupina uživatelů existuje.
            // _groupRepository.ThrowIfNotExists(request.GroupId);
            //
            // // Načtu kolekci uživatelských rolí uživatele v dané skupině a zpracuju na stránkovanou kolekci.
            // var result = await _groupRoleRepository.GetForUserInGroup(request.UserId, request.GroupId)
            //     .ProjectTo<GroupRoleDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);
            //
            // return result;
            return null;
        }
    }
}