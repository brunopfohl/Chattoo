using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Users.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;

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
        private readonly IUserRepository _userRepository;
        private readonly ChannelManager _channelManager;

        public GetUsersForChannelQueryHandler(IMapper mapper,
            IUserRepository userRepository, ChannelManager channelManager)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _channelManager = channelManager;
        }

        public override async Task<PaginatedList<UserDto>> Handle(GetUsersForChannelQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            
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