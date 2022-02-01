using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.GroupRoles.Queries.GetForUser
{
    /// <summary>
    /// Dotaz na skupiny, do kterých patří daný uživatel.
    /// </summary>
    public class GetGroupsForUserQuery : PaginatedQuery<GroupDto>
    {
        public string UserId { get; set; }
    }

    public class GetGroupsForUserQueryHandler : PaginatedQueryHandler<GetGroupsForUserQuery, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GetGroupsForUserQueryHandler(IGroupRepository groupRepository, IUserRepository userRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<PaginatedList<GroupDto>> Handle(GetGroupsForUserQuery request, CancellationToken cancellationToken)
        {
            // // Ověřím, zda-li uživatel existuje.
            // _userRepository.ThrowIfNotExists(request.UserId);
            //
            // // Načtu kolekci skupin, do kterých spadá uživatel a zpracuju na stránkovanou kolekci.
            // var result = await _groupRepository.GetByUserId(request.UserId)
            //     .ProjectTo<GroupDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);
            //
            // return result;
            return null;
        }
    }
}