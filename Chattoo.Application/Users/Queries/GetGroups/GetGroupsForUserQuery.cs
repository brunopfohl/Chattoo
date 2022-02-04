using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries
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
        private readonly ICurrentUserService _currentUserService;

        public GetGroupsForUserQueryHandler(IGroupRepository groupRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<PaginatedList<GroupDto>> Handle(GetGroupsForUserQuery request, CancellationToken cancellationToken)
        {
            // TODO: Pro admina přístup k libovolnému uživateli.
            // Stáhnu kolekci skupin, do kterých je uživatel zapojen.
            var groups = _groupRepository.GetByUserId(_currentUserService.User?.Id);
            
            // Načtu kolekci skupin, do kterých spadá uživatel a zpracuju na stránkovanou kolekci.
            var result = await groups
                .ProjectTo<GroupDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}