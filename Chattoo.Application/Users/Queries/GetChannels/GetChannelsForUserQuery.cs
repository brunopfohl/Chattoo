using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Users.Queries
{
    /// <summary>
    /// Dotaz na skupiny, do kterých patří daný uživatel.
    /// </summary>
    public class GetChannelsForUserQuery : PaginatedQuery<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, jehož komunikační kanály jsou dotazovány.
        /// </summary>
        public string UserId { get; set; }
    }

    public class GetChannelsForUserQueryHandler : PaginatedQueryHandler<GetChannelsForUserQuery, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public GetChannelsForUserQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _currentUserService = currentUserService;
        }

        public override async Task<PaginatedList<CommunicationChannelDto>> Handle(GetChannelsForUserQuery request, CancellationToken cancellationToken)
        {
            // TODO: Pro admina přístup k libovolnému uživateli.
            // Stáhnu kolekci komunikačních kanálů, do kterých je uživatel zapojen.
            var channels = _communicationChannelRepository.GetByUserId(_currentUserService.User?.Id);
            
           // Načtu kolekci komunikačních kanálů uživatele a zpracuju na stránkovanou kolekci.
           var result = await channels
                .ProjectTo<CommunicationChannelDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
           
            return result;
        }
    }
}