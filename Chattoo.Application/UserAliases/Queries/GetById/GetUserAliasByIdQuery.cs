using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.UserAliases.DTOs;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.UserAliases.Queries.GetById
{
    /// <summary>
    /// Dotaz na uživatelskou přezdívku s daným Id.
    /// </summary>
    public class GetUserAliasByIdQuery : IRequest<UserAliasDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetUserAliasByIdQueryHandler : IRequestHandler<GetUserAliasByIdQuery, UserAliasDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserAliasRepository _userAliasRepository;

        public GetUserAliasByIdQueryHandler(IMapper mapper, IUserAliasRepository userAliasRepository)
        {
            _mapper = mapper;
            _userAliasRepository = userAliasRepository;
        }

        public async Task<UserAliasDto> Handle(GetUserAliasByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu uživatelskou přezdívku z datového zdroje.
            var userAlias = await _userAliasRepository.GetByIdAsync<UserAliasDto>(request.Id, true);
            return userAlias;
        }
    }
}