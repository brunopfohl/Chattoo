using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Application.UserAliases.DTOs;
using Chattoo.Application.Users.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Users.Queries.GetForCommunicationChannel
{
    /// <summary>
    /// Dotaz na uživatelskou přezdívku s daným Id.
    /// </summary>
    public class GetUsersForCommunicationChannelQuery : PaginatedQuery<UserDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string ChannelId { get; set; }
    }
    
    public class GetUsersForCommunicationChannelQueryHandler : PaginatedQueryHandler<GetUsersForCommunicationChannelQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public GetUsersForCommunicationChannelQueryHandler(IMapper mapper, IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public override async Task<PaginatedList<UserDto>> Handle(GetUsersForCommunicationChannelQuery request, CancellationToken cancellationToken)
        {
            // Ověřím si, že komunikační kanál existuje.
            _communicationChannelRepository.ThrowIfNotExists(request.ChannelId);

            // Načtu kolekci uživatelů v dané skupině a zpracuju na stránkovanou kolekci.
            var result = await _userRepository.GetByChannelId(request.ChannelId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}