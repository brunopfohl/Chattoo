using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannelRoles.DTOs;
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

        public GetCommunicationChannelRolesForUserInChannelQueryHandler(IMapper mapper, IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public override async Task<PaginatedList<CommunicationChannelRoleDto>> Handle(GetCommunicationChannelRolesForUserInChannelQuery request, CancellationToken cancellationToken)
        {
            // // Ověřím, zda-li uživatel existuje.
            // _userRepository.ThrowIfNotExists(request.UserId);
            // // Ověřím, zda-li komunikační kanál existuje.
            // _communicationChannelRepository.ThrowIfNotExists(request.ChannelId);
            //
            // // Načtu kolekci rolí uživatele v komunikačním kanálu a zpracuju na stránkovanou kolekci.
            // var result = await _communicationChannelRoleRepository.GetForUserInChannel(request.UserId, request.ChannelId)
            //     .ProjectTo<CommunicationChannelRoleDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);
            //
            // return result;
            return null;
        }
    }
}