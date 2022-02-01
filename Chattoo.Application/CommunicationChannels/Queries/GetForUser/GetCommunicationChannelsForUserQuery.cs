using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CommunicationChannels.Queries.GetForUser
{
    /// <summary>
    /// Dotaz na skupiny, do kterých patří daný uživatel.
    /// </summary>
    public class GetCommunicationChannelsForUserQuery : PaginatedQuery<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, jehož komunikační kanály jsou dotazovány.
        /// </summary>
        public string UserId { get; set; }
    }

    public class GetCommunicationChannelsForUserQueryHandler : PaginatedQueryHandler<GetCommunicationChannelsForUserQuery, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public GetCommunicationChannelsForUserQueryHandler(IMapper mapper, IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public override async Task<PaginatedList<CommunicationChannelDto>> Handle(GetCommunicationChannelsForUserQuery request, CancellationToken cancellationToken)
        {
            // // Ověřím, zda-li uživatel skutečně existuje.
            // _userRepository.ThrowIfNotExists(request.UserId);
            //
            // // Načtu kolekci komunikačních kanálů uživatele a zpracuju na stránkovanou kolekci.
            // var result = await _communicationChannelRepository.GetByUserId(request.UserId)
            //     .ProjectTo<CommunicationChannelDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);
            //
            // return result;
            return null;
        }
    }
}