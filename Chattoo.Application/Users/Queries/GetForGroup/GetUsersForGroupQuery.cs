using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Users.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries.GetForGroup
{
    /// <summary>
    /// Dotaz na získání uživatelů z dané skupiny.
    /// </summary>
    public class GetUsersForGroupQuery : PaginatedQuery<UserDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, jejíž uživatele tážeme.
        /// </summary>
        public string GroupId { get; set; }
    }

    public class GetUsersForGroupQueryHandler : PaginatedQueryHandler<GetUsersForGroupQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public GetUsersForGroupQueryHandler(IUserRepository userRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public override async Task<PaginatedList<UserDto>> Handle(GetUsersForGroupQuery request, CancellationToken cancellationToken)
        {
            // Ověřím, zda-li skupina uživatelů skutečně existuje.
            //_groupRepository.ThrowIfNotExists(request.GroupId);

            // Načtu seznam uživatelů ze skupiny.
            var result = await _userRepository.GetByGroupId(request.GroupId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}