using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CalendarEvents.Queries
{
    /// <summary>
    /// Dotaz na kalendářní události z komunikačního kanálu.
    /// </summary>
    public class GetCalendarEventsForCommunicationChannelQuery : PaginatedQuery<CalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, jehož kalendářní události se mají vrátit.
        /// </summary>
        public string CommunicationChannelId { get; set; }
    }

    public class GetCalendarEventsForUserQueryHandler : PaginatedQueryHandler<GetCalendarEventsForCommunicationChannelQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;

        public GetCalendarEventsForUserQueryHandler(IMapper mapper, ICalendarEventRepository calendarEventRepository, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _calendarEventRepository = calendarEventRepository;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetCalendarEventsForCommunicationChannelQuery request, CancellationToken cancellationToken)
        {
            // Ověřím, zda-li komunikační kanál skutečně existuje.
            // _communicationChannelRepository.ThrowIfNotExists(request.CommunicationChannelId);
            //
            // // Načtu kolekci kalendářních událostí komunikačního kanálu a zpracuju na stránkovanou kolekci.
            // var result = await _calendarEventRepository.GetByCommunicationChannelId(request.CommunicationChannelId)
            //     .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);
            //
            // return result;
            return null;
        }
    }
}