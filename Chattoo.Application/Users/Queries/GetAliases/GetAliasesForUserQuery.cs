using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries
{
    /// <summary>
    /// Dotaz přezdívky daného uživatele.
    /// </summary>
    public class GetAliasesForUserQuery : PaginatedQuery<UserAliasDto>
    {
        public string UserId { get; set; }
    }

    public class GetAliasesForUserQueryHandler : PaginatedQueryHandler<GetAliasesForUserQuery, UserAliasDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAliasesForUserQueryHandler(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<PaginatedList<UserAliasDto>> Handle(GetAliasesForUserQuery request, CancellationToken cancellationToken)
        {
            // TODO: Admin má právo na všechny

            var aliases = _currentUserService.User.Aliases.AsQueryable();
            
            var result = await aliases
                .ProjectTo<UserAliasDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}