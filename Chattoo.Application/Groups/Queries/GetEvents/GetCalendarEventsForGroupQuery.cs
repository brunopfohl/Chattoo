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
        private readonly IGroupRepository _groupRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public GetCalendarEventsForCommunicationChannelQueryHandler(IMapper mapper, ICalendarEventRepository calendarEventRepository, GetByIdUserSafeService getByIdUserSafeService, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _calendarEventRepository = calendarEventRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
            _groupRepository = groupRepository;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetCalendarEventsForGroupQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.GroupId);

            var events = _calendarEventRepository.GetByGroupId(group.Id);
            
            // Načtu kolekci kalendářních událostí komunikačního kanálu a zpracuju na stránkovanou kolekci.
            var result = await events
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            
            return result;
        }
    }
}