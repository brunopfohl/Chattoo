using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannelRoles.DTOs;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CommunicationChannels.Queries.GetForUserInChannel
{
    /// <summary>
    /// Dotaz na uživatelské role uživatele v komunikačním kanálu.
    /// </summary>
    public class GetCommunicationChannelRolesForUserInChannelQuery : PaginatedQuery<CommunicationChannelRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, jehož role jsou dotazovány.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, ve kterém jsou hledány uživatelské role.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class GetCommunicationChannelRolesForUserInChannelQueryHandler : PaginatedQueryHandler<GetCommunicationChannelRolesForUserInChannelQuery, CommunicationChannelRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICommunicationChannelRoleRepository _communicationChannelRoleRepository;

        public GetCommunicationChannelRolesForUserInChannelQueryHandler(IMapper mapper, IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository, ICommunicationChannelRoleRepository communicationChannelRoleRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
            _communicationChannelRoleRepository = communicationChannelRoleRepository;
        }

        public override async Task<PaginatedList<CommunicationChannelRoleDto>> Handle(GetCommunicationChannelRolesForUserInChannelQuery request, CancellationToken cancellationToken)
        {
            // Nejdřív načtu uživatele s Id z požadavku, abych mohl určit, jestli uživatel skutečně existuje.
            var user = await _userRepository.GetByIdAsync(request.UserId);

            // Pokud se nepodařilo dohledat uživatele, vyhodím výjimku.
            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }
            
            // Načtu komunikační kanál.
            var channel = await _communicationChannelRepository.GetByIdAsync(request.ChannelId);

            // Pokud se nepodařilo dohledat komunikační kanál, vyhodím výjimku.
            if (channel is null)
            {
                throw new NotFoundException(nameof(CommunicationChannel), request.ChannelId);
            }

            // Načtu kolekci rolí uživatele v komunikačním kanálu a zpracuju na stránkovanou kolekci.
            var result = await _communicationChannelRoleRepository.GetForUserInChannel(request.UserId, request.ChannelId)
                .ProjectTo<CommunicationChannelRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}