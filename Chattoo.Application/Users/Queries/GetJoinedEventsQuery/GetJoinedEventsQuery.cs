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

namespace Chattoo.Application.Users.Queries.GetJoinedEventsQuery.cs
{
    public class GetJoinedEventsQuery : PaginatedQuery<CalendarEventDto>
    {
        
    }

    public class GetJoinedEventsQueryHandler : PaginatedQueryHandler<GetJoinedEventsQuery, CalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICalendarEventRepository _eventRepository;
        private readonly ICurrentUserService _currentUserService;
        
        public GetJoinedEventsQueryHandler(ICalendarEventRepository eventRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public override async Task<PaginatedList<CalendarEventDto>> Handle(GetJoinedEventsQuery request,
            CancellationToken cancellationToken)
        {
            var events = _eventRepository
                .GetAll()
                .Where(e => e.Participants.Any(p => p.UserId == _currentUserService.User.Id));
            
            var result = await events
                .OrderByDescending(m => m.CreatedAt)
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListOrderedAsync(request.PageNumber, request.PageSize, m => m.CreatedAt);

            return result;
        }
    }
}