using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.UserAliases.DTOs;
using Chattoo.Domain.Entities;
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
            var userAlias = await _userAliasRepository.GetByIdAsync(request.Id);

            // Pokud se uživatelskou přezdívku s daným Id nepodařilo dohledat, vracím chybu.
            if (userAlias is null)
            {
                throw new NotFoundException(nameof(UserAlias), request.Id);
            }

            // Převedu entitu na dto.
            var result = _mapper.Map<UserAliasDto>(userAlias);

            return result;
        }
    }
}