using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CalendarEvents.Queries.Get
{
    public class GetCalendarEventsQuery : PaginatedQuery<CalendarEventDto>
    {
        
    }

    public class GetCalendarEventsQueryHandler : PaginatedQueryHandler<GetCalendarEventsQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarEventRepository _eventRepository;
        private readonly ICurrentUserService _currentUserService;
        
        public GetCalendarEventsQueryHandler(ICurrentUserService currentUserService, ICalendarEventRepository eventRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetCalendarEventsQuery request, CancellationToken cancellationToken)
        {
            var joinedChannelIds = _currentUserService.User.Channels.Select(ch => ch.ChannelId);
            var joinedGroupIds = _currentUserService.User.Groups.Select(ch => ch.GroupId);
            
            // Načtu kolekci uživatelů v dané skupině a zpracuju na stránkovanou kolekci.
            var events = _eventRepository
                .GetAll()
                .Where(e =>
                    e.CommunicationChannelId != null && joinedChannelIds.Contains(e.CommunicationChannelId) ||
                    e.GroupId != null && joinedGroupIds.Contains(e.GroupId)
                );
            
            var result = await events
                .OrderByDescending(m => m.CreatedAt)
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListOrderedAsync(request.PageNumber, request.PageSize, m => m.CreatedAt);

            return result;
        }
    }
}