using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    /// <summary>
    /// Dotaz na kalendářní události z komunikačního kanálu.
    /// </summary>
    public class GetCalendarEventsForCommunicationChannelQuery : PaginatedQuery<CalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, jehož kalendářní události se mají vrátit.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class GetCalendarEventsForCommunicationChannelQueryHandler : PaginatedQueryHandler<GetCalendarEventsForCommunicationChannelQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;

        public GetCalendarEventsForCommunicationChannelQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetCalendarEventsForCommunicationChannelQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var events = _channelManager.GetEvents(channel);
            
            // Načtu kolekci kalendářních událostí komunikačního kanálu a zpracuju na stránkovanou kolekci.
            var result = await events
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}