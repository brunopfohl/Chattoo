using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    /// <summary>
    /// Dotaz na zprávy z komunikačního kanálu.
    /// </summary>
    public class GetCommunicationChannelMessagesForChannelQuery : PaginatedQuery<CommunicationChannelMessageDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, ve kterém jsou hledány zprávy.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class GetCommunicationChannelMessagesForUserInChannelQueryHandler : PaginatedQueryHandler<GetCommunicationChannelMessagesForChannelQuery, CommunicationChannelMessageDto>
    {
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;

        public GetCommunicationChannelMessagesForUserInChannelQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public override async Task<PaginatedList<CommunicationChannelMessageDto>> Handle(GetCommunicationChannelMessagesForChannelQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var messages = channel.Messages.AsQueryable();
            
            // Načtu kolekci rolí uživatele v komunikačním kanálu a zpracuju na stránkovanou kolekci.
            var result = await messages
                .OrderByDescending(m => m.CreatedAt)
                .ProjectTo<CommunicationChannelMessageDto>(_mapper.ConfigurationProvider)
                .PaginatedListOrderedAsync(request.PageNumber, request.PageSize, m => m.CreatedAt);
            
            return result;
        }
    }
}