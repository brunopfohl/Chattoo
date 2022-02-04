using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannelRoles.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries
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
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public GetCommunicationChannelRolesForUserInChannelQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public override async Task<PaginatedList<CommunicationChannelRoleDto>> Handle(GetCommunicationChannelRolesForUserInChannelQuery request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);
            
            var roles = channel.Roles
                .Where(r => r.Users.Any(u => u.Id == request.UserId)).AsQueryable();
            
            // Načtu kolekci rolí uživatele v komunikačním kanálu a zpracuju na stránkovanou kolekci.
            var result = await roles
                .ProjectTo<CommunicationChannelRoleDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}