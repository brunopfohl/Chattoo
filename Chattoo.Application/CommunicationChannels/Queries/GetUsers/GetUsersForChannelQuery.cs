using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Common.Services;
using Chattoo.Application.Users.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CommunicationChannels.Queries.GetUsers
{
    public class GetUsersForChannelQuery : PaginatedQuery<UserDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string ChannelId { get; set; }
    }
    
    public class GetUsersForChannelQueryHandler : PaginatedQueryHandler<GetUsersForChannelQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly IUserRepository _userRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public GetUsersForChannelQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository,
            GetByIdUserSafeService getByIdUserSafeService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
            _userRepository = userRepository;
        }

        public override async Task<PaginatedList<UserDto>> Handle(GetUsersForChannelQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);
            
            // Načtu uživatele z komunikačního kanálu.
            var users = _userRepository.GetByChannelId(channel.Id);

            // Načtu kolekci uživatelů v dané skupině a zpracuju na stránkovanou kolekci.
            var result = await users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}