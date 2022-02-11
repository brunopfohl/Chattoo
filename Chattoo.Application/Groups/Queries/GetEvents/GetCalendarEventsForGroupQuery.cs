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

namespace Chattoo.Application.Groups.Queries
{
    /// <summary>
    /// Dotaz na kalendářní události z komunikačního kanálu.
    /// </summary>
    public class GetCalendarEventsForGroupQuery : PaginatedQuery<CalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, jehož kalendářní události se mají vrátit.
        /// </summary>
        public string GroupId { get; set; }
    }

    public class GetCalendarEventsForCommunicationChannelQueryHandler : PaginatedQueryHandler<GetCalendarEventsForGroupQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly GroupManager _groupManager;

        public GetCalendarEventsForCommunicationChannelQueryHandler(IMapper mapper, GroupManager groupManager)
        {
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetCalendarEventsForGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.GroupId);

            var events = await _groupManager.GetEvents(group);
            
            // Načtu kolekci kalendářních událostí komunikačního kanálu a zpracuju na stránkovanou kolekci.
            var result = await events
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}