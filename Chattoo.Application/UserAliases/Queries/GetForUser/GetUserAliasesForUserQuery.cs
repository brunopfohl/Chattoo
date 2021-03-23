using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.UserAliases.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.UserAliases.Queries.GetForUser
{
    /// <summary>
    /// Dotaz přezdívky daného uživatele.
    /// </summary>
    public class GetUserAliasesForUserQuery : PaginatedQuery<UserAliasDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserAliasesForUserQueryHandler : PaginatedQueryHandler<GetUserAliasesForUserQuery, UserAliasDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserAliasRepository _userAliasRepository;

        public GetUserAliasesForUserQueryHandler(IUserRepository userRepository, IMapper mapper, IUserAliasRepository userAliasRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userAliasRepository = userAliasRepository;
        }

        public override async Task<PaginatedList<UserAliasDto>> Handle(GetUserAliasesForUserQuery request, CancellationToken cancellationToken)
        {
            // Nejdřív načtu uživatele s Id z požadavku, abych mohl určit, jestli uživatel skutečně exituje.
            var user = await _userRepository.GetByIdAsync(request.UserId);

            // Pokud se mi uživatele nepodařilo vyhledat, vyhodím výjimku.
            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            // Načtu přezdívek uživatele a zpracuju na stránkovanou kolekci.
            var result = await _userAliasRepository.GetByUserId(request.UserId)
                .ProjectTo<UserAliasDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}